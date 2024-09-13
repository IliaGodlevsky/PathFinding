using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Linq;
using Pathfinding.Shared.Extensions;
using System.Data;
using System;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using CommunityToolkit.Mvvm.Messaging;
using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class RunsTableView : TableView
    {
        private readonly CompositeDisposable disposables = new();
        private readonly RunsTableViewModel viewModel;

        public RunsTableView(RunsTableViewModel viewModel,
            [KeyFilter(KeyFilters.Views)]IMessenger messenger)
        {
            Initialize();
            viewModel.Runs.ActOnEveryObject(OnAdded, OnRemoved);
            this.Events().CellActivated
                .Select(x => GetRunModel(x.Row))
                .Do(x => messenger.Send(new OpenAlgorithmRunViewMessage()))
                .InvokeCommand(viewModel, x => x.ActivateRunCommand)
                .DisposeWith(disposables);
            this.Events().SelectedCellChanged
                .Where(x => x.NewRow > -1)
                .Select(x => (
                            MultiSelectedRegions.Count > 0
                            ? MultiSelectedRegions
                                .SelectMany(x => (x.Rect.Top, x.Rect.Bottom - 1).Iterate())
                                .Select(GetRunModel)
                            : GetRunModel(x.NewRow).Enumerate()
                            )
                          .ToArray())
                .BindTo(viewModel, x => x.Selected)
                .DisposeWith(disposables);
            this.viewModel = viewModel;
        }

        private RunModel GetRunModel(int selectedRow)
        {
            return new()
            {
                RunId = (int)table.Rows[selectedRow]["Id"],
                Name = (string)table.Rows[selectedRow]["Algorithm"],
                Visited = (int)table.Rows[selectedRow]["Visited"],
                Steps = (int)table.Rows[selectedRow]["Steps"],
                Cost = (double)table.Rows[selectedRow]["Cost"],
                Elapsed = (TimeSpan)table.Rows[selectedRow]["Elapsed"],
                StepRule = (string)table.Rows[selectedRow]["Step"],
                Heuristics = (string)table.Rows[selectedRow]["Logic"],
                Spread = (string)table.Rows[selectedRow]["Spread"],
                Status = (string)table.Rows[selectedRow]["Status"]
            };
        }

        private void OnAdded(RunModel model)
        {
            table.Rows.Add(model.RunId, model.Name,  model.Visited, 
                model.Steps, model.Cost, model.Elapsed, model.StepRule, 
                model.Heuristics, model.Spread, model.Status);
            table.AcceptChanges();
            SetNeedsDisplay();
            Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
        }

        private void OnRemoved(RunModel model)
        {
            var row = table.Rows.Find(model.RunId);
            var index = table.Rows.IndexOf(row);
            if (row != null)
            {
                row.Delete();
                table.AcceptChanges();
                MultiSelectedRegions.Clear();
                if (table.Rows.Count > 0)
                {
                    SelectedCellChangedEventArgs args = index == table.Rows.Count
                        ? args = new(table, 0, 0, index, index - 1)
                        : new(table, 0, 0, index, index);
                    OnSelectedCellChanged(args);
                    SetSelection(0, args.NewRow, false);
                }
                SetNeedsDisplay();
                Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
            }
        }
    }
}
