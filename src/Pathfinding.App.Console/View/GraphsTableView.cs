﻿using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.ViewModel.Interface;
using Pathfinding.Shared.Extensions;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Data;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class GraphsTableView
    {
        private readonly IGraphTableViewModel viewModel;
        private readonly CompositeDisposable disposables = new();
        private readonly Dictionary<int, IDisposable> modelChangingSubs = new();
        private readonly IMessenger messenger;

        public GraphsTableView(IGraphTableViewModel viewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger) : this()
        {
            this.viewModel = viewModel;
            this.messenger = messenger;
            viewModel.Graphs.ActOnEveryObject(AddToTable, RemoveFromTable)
                .DisposeWith(disposables);
            this.Events().Initialized
                .Select(x => Unit.Default)
                .InvokeCommand(viewModel, x => x.LoadGraphsCommand)
                .DisposeWith(disposables);
            this.Events().CellActivated
                .Where(x => x.Row < table.Rows.Count)
                .Select(x => GetGraphId(x.Row))
                .InvokeCommand(viewModel, x => x.ActivateGraphCommand)
                .DisposeWith(disposables);
            this.Events().KeyPress
                .Where(x => x.KeyEvent.Key.HasFlag(Key.A)
                    && x.KeyEvent.Key.HasFlag(Key.CtrlMask))
                .Throttle(TimeSpan.FromMilliseconds(150))
                .Select(x => MultiSelectedRegions
                        .SelectMany(x => (x.Rect.Top, x.Rect.Bottom - 1).Iterate())
                        .Select(GetGraphId)
                        .ToArray())
                .InvokeCommand(viewModel, x => x.SelectGraphsCommand)
                .DisposeWith(disposables);
            this.Events().SelectedCellChanged
                .Where(x => x.NewRow > -1 && x.NewRow < table.Rows.Count)
                .Select(x => GetAllSelectedCells().Select(x => x.Y)
                    .Distinct().Select(GetGraphId).ToArray())
                .InvokeCommand(viewModel, x => x.SelectGraphsCommand)
                .DisposeWith(disposables);
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(x => messenger.Send(new CloseAlgorithmRunFieldViewMessage()))
                .Select(x => x.MouseEvent.Y + RowOffset - headerLinesConsumed)
                .Where(x => x >= 0 && x < Table.Rows.Count && x == SelectedRow)
                .Select(x => GetGraphId(x).Enumerate().ToArray())
                .InvokeCommand(viewModel, x => x.SelectGraphsCommand)
                .DisposeWith(disposables);
        }

        private int GetGraphId(int selectedRow)
        {
            return (int)Table.Rows[selectedRow][IdCol];
        }

        private void AddToTable(GraphInfoModel model)
        {
            Application.MainLoop.Invoke(() =>
            {
                table.Rows.Add(model.GetProperties());
                table.AcceptChanges();
                SetNeedsDisplay();
                var composite = new CompositeDisposable();
                BindTo(model, ObstaclesCol, x => x.ObstaclesCount).DisposeWith(composite);
                BindTo(model, NameCol, x => x.Name).DisposeWith(composite);
                BindTo(model, StatusCol, x => x.Status).DisposeWith(composite);
                BindTo(model, SmoothCol, x => x.SmoothLevel).DisposeWith(composite);
                BindTo(model, NeighborsCol, x => x.Neighborhood).DisposeWith(composite);
                modelChangingSubs.Add(model.Id, composite);
                SetCursorInvisible();
            });
        }

        private IDisposable BindTo<T>(GraphInfoModel model, string column,
            Expression<Func<GraphInfoModel, T>> expression)
        {
            return model.WhenAnyValue(expression)
                .Do(x => Update(model.Id, column, x))
                .Subscribe();
        }

        private void Update<T>(int id, string column, T value)
        {
            Application.MainLoop.Invoke(() =>
            {
                var row = table.Rows.Find(id);
                row[column] = value;
                table.AcceptChanges();
                SetNeedsDisplay();
                SetCursorInvisible();
            });
        }

        private void RemoveFromTable(GraphInfoModel model)
        {
            Application.MainLoop.Invoke(() =>
            {
                var row = table.Rows.Find(model.Id);
                var index = table.Rows.IndexOf(row);
                row.Delete();
                table.AcceptChanges();
                modelChangingSubs[model.Id].Dispose();
                modelChangingSubs.Remove(model.Id);
                MultiSelectedRegions.Clear();
                if (table.Rows.Count > 0)
                {
                    SelectedCellChangedEventArgs args = index == table.Rows.Count
                        ? new(table, 0, 0, index, index - 1)
                        : new(table, 0, 0, index, index);
                    OnSelectedCellChanged(args);
                    SetSelection(0, args.NewRow, false);
                }
                SetNeedsDisplay();
                SetCursorInvisible();
            });
        }

        private static void SetCursorInvisible()
        {
            Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            modelChangingSubs.ForEach(x => x.Value.Dispose());
            base.Dispose(disposing);
        }
    }
}
