using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Shared.Extensions;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphsTableView
    {
        private readonly GraphTableViewModel viewModel;
        private readonly CompositeDisposable disposables = new();
        private readonly Dictionary<int, IDisposable> modelChangingSubs = new();
        private readonly IMessenger messenger;

        public GraphsTableView(GraphTableViewModel viewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger) : this()
        {
            this.viewModel = viewModel;
            this.messenger = messenger;
            viewModel.Graphs.ActOnEveryObject(AddToTable, RemoveFromTable)
                .DisposeWith(disposables);
            viewModel.LoadGraphs();
            this.Events().CellActivated
                .Where(x => x.Row < table.Rows.Count)
                .Select(x => GetParametresModel(x.Row))
                .InvokeCommand(viewModel, x => x.ActivateGraphCommand)
                .DisposeWith(disposables);
            this.Events().SelectedCellChanged
                .Where(x => x.NewRow > -1 && x.NewRow < table.Rows.Count)
                .Select(x => GetAllSelectedCells().DistinctBy(y => y.Y)
                        .Select(z => GetParametresModel(z.Y)).ToArray())
                .BindTo(viewModel, x => x.SelectedGraphs)
                .DisposeWith(disposables);
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(x => messenger.Send(new CloseAlgorithmRunFieldViewMessage()))
                .Select(x => x.MouseEvent.Y + RowOffset - headerLinesConsumed)
                .Where(x => x >= 0 && x < Table.Rows.Count && x == SelectedRow)
                .Select(x => GetParametresModel(x).Enumerate().ToArray())
                .BindTo(viewModel, x => x.SelectedGraphs)
                .DisposeWith(disposables);
        }

        private GraphInfoModel GetParametresModel(int selectedRow)
        {
            return new()
            {
                Width = (int)table.Rows[selectedRow][WidthCol],
                Length = (int)table.Rows[selectedRow][LengthCol],
                Name = (string)table.Rows[selectedRow][NameCol],
                SmoothLevel = (string)table.Rows[selectedRow][SmoothCol],
                Neighborhood = (string)table.Rows[selectedRow][NeighborsCol],
                Id = (int)table.Rows[selectedRow][IdCol],
                Obstacles = (int)table.Rows[selectedRow][ObstaclesCol],
                Status = (string)table.Rows[selectedRow][StatusCol]
            };
        }

        private void AddToTable(GraphInfoModel model)
        {
            Application.MainLoop.Invoke(() =>
            {
                table.Rows.Add(model.GetProperties());
                table.AcceptChanges();
                SetNeedsDisplay();
                var composite = new CompositeDisposable();
                BindTo(model, ObstaclesCol, x => x.Obstacles).DisposeWith(composite);
                BindTo(model, StatusCol, x => x.Status).DisposeWith(composite);
                BindTo(model, NameCol, x => x.Name).DisposeWith(composite);
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
            var row = table.Rows.Find(id);
            row[column] = value;
            table.AcceptChanges();
            SetNeedsDisplay();
            SetCursorInvisible();
        }

        private void RemoveFromTable(GraphInfoModel model)
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
