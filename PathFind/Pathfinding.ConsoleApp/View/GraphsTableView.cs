using Pathfinding.ConsoleApp.ViewModel;
using System.Linq;
using System.Reactive.Disposables;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using Pathfinding.ConsoleApp.Model;
using Terminal.Gui;
using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using CommunityToolkit.Mvvm.Messaging;
using System.Data;
using System;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphsTableView
    {
        private readonly GraphTableViewModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private readonly IMessenger messenger;

        public GraphsTableView(GraphTableViewModel viewModel, 
            [KeyFilter(KeyFilters.Views)]IMessenger messenger)
        {
            this.viewModel = viewModel;
            this.messenger = messenger;
            Initialize();
            viewModel.Graphs.ActOnEveryObject(AddToTable, RemoveFromTable)
                .DisposeWith(disposables);
            viewModel.LoadGraphs();
            this.Events().CellActivated
                .Select(x => GetModel(x.Row))
                .DistinctUntilChanged(x => x.Id)
                .BindTo(this.viewModel, x => x.Activated)
                .DisposeWith(disposables);
            this.Events().SelectedCellChanged
                .Where(x => x.NewRow > -1)
                .Select(x => GetModel(x.NewRow))
                .BindTo(viewModel, x => x.Selected)
                .DisposeWith(disposables);
            //this.Events().MouseClick
            //    .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
            //    .Where(x => SelectedRow > -1)
            //    .Select(x => GetModel(SelectedRow))
            //    .BindTo(viewModel, x => x.Selected)
            //    .DisposeWith(disposables);
        }

        private GraphParametresModel GetModel(int selectedRow)
        {
            return new GraphParametresModel()
            {
                Width = (int)table.Rows[selectedRow]["Width"],
                Length = (int)table.Rows[selectedRow]["Length"],
                Name = (string)table.Rows[selectedRow]["Name"],
                Id = (int)table.Rows[selectedRow]["Id"],
                Obstacles = (int)table.Rows[selectedRow]["Obstacles"]
            };
        }

        private void AddToTable(GraphParametresModel model)
        {
            table.Rows.Add(model.Id, model.Name, model.Width, model.Length, model.Obstacles);
            table.AcceptChanges();
            SetNeedsDisplay();
            Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
            model.WhenAnyValue(c => c.Obstacles)
                .Do(x => UpdateGraphInTable(model.Id, x))
                .Subscribe();
        }

        private void UpdateGraphInTable(int id, int obstacles)
        {
            var row = table.Rows.Find(id);
            row["Obstacles"] = obstacles;
            table.AcceptChanges();
        }

        private void RemoveFromTable(GraphParametresModel model)
        {
            var row = table.Rows.Find(model.Id);
            var index = table.Rows.IndexOf(row);
            row.Delete();
            table.AcceptChanges();
            if (table.Rows.Count > 0)
            {
                SelectedCellChangedEventArgs args = null;
                if (index == table.Rows.Count)
                {
                    args = new SelectedCellChangedEventArgs(table, 0, 0, index, index - 1);
                }
                else
                {
                    args = new SelectedCellChangedEventArgs(table, 0, 0, index, index);
                }
                OnSelectedCellChanged(args);
                SetSelection(0, args.NewRow, false);
            }
            SetNeedsDisplay();
            Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
        }
    }
}
