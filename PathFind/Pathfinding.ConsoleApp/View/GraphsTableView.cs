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
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            this.viewModel = viewModel;
            this.messenger = messenger;
            Initialize();
            viewModel.Graphs.ActOnEveryObject(AddToTable, RemoveFromTable)
                .DisposeWith(disposables);
            viewModel.LoadGraphs();
            this.Events().CellActivated
                .Select(x => GetParametresModel(x.Row))
                .DistinctUntilChanged(x => x.Id)
                .InvokeCommand(viewModel, x => x.ActivateGraphCommand)
                .DisposeWith(disposables);
            this.Events().SelectedCellChanged
                .Where(x => x.NewRow > -1)
                .Select(x => (
                            MultiSelectedRegions.Count > 0
                            ? MultiSelectedRegions
                                .SelectMany(x => (x.Rect.Top, x.Rect.Bottom - 1).Iterate())
                                .Select(GetParametresModel)
                            : GetParametresModel(x.NewRow).Enumerate()
                            )
                          .ToArray())
                .BindTo(viewModel, x => x.Selected)
                .DisposeWith(disposables);
            MouseClick += MouseEntered;
        }

        private void MouseEntered(MouseEventArgs e)
        {
            messenger.Send(new CloseAlgorithmRunViewMessage());
        }

        private GraphInfoModel GetParametresModel(int selectedRow)
        {
            return new()
            {
                Width = (int)table.Rows[selectedRow]["Width"],
                Length = (int)table.Rows[selectedRow]["Length"],
                Name = (string)table.Rows[selectedRow]["Name"],
                Id = (int)table.Rows[selectedRow]["Id"],
                Obstacles = (int)table.Rows[selectedRow]["Obstacles"]
            };
        }

        private void AddToTable(GraphInfoModel model)
        {
            table.Rows.Add(model.Id, model.Name,
                model.Width, model.Length, model.Obstacles);
            table.AcceptChanges();
            SetNeedsDisplay();
            Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
            var sub = model.WhenAnyValue(c => c.Obstacles)
                .Do(x => UpdateGraphInTable(model.Id, x))
                .Subscribe();
            modelChangingSubs.Add(model.Id, sub);
        }

        private void UpdateGraphInTable(int id, int obstacles)
        {
            var row = table.Rows.Find(id);
            row["Obstacles"] = obstacles;
            table.AcceptChanges();
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
            Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
        }
    }
}
