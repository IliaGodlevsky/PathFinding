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
using Pathfinding.ConsoleApp.Messages;
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
            this.Events()
                .CellActivated
                .Select(x => new GraphParametresModel()
                {
                    Width = (int)table.Rows[x.Row]["Width"],
                    Length = (int)table.Rows[x.Row]["Length"],
                    Name = (string)table.Rows[x.Row]["Name"],
                    Id = (int)table.Rows[x.Row]["Id"],
                    Obstacles = (int)table.Rows[x.Row]["Obstacles"]
                })
                .DistinctUntilChanged(x => x.Id)
                .BindTo(this.viewModel, x => x.Activated)
                .DisposeWith(disposables);
            this.Events().SelectedCellChanged
                .Where(x => x.NewRow > -1)
                .Select(x => new GraphParametresModel()
                {
                    Width = (int)table.Rows[x.NewRow]["Width"],
                    Length = (int)table.Rows[x.NewRow]["Length"],
                    Name = (string)table.Rows[x.NewRow]["Name"],
                    Id = (int)table.Rows[x.NewRow]["Id"],
                    Obstacles = (int)table.Rows[x.NewRow]["Obstacles"]
                })
                .BindTo(viewModel, x => x.Selected)
                .DisposeWith(disposables);
            viewModel.Graphs.ActOnEveryObject(AddToTable, RemoveFromTable);
        }

        private void AddToTable(GraphParametresModel x)
        {
            table.Rows.Add(x.Id, x.Name, x.Width, x.Length, x.Obstacles);
            table.AcceptChanges();
            SetNeedsDisplay();
            Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
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
