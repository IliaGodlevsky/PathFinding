using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Shared.Extensions;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Data;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class RunsTableView : TableView
    {
        private readonly CompositeDisposable disposables = new();

        public RunsTableView(RunsTableViewModel viewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Initialize();
            viewModel.Runs.ActOnEveryObject(OnAdded, OnRemoved);
            this.Events().CellActivated
                .Where(x => x.Row <= table.Rows.Count - 1)
                .Do(x => messenger.Send(new OpenAlgorithmRunViewMessage()))
                .Select(x => GetRunModel(x.Row))
                .InvokeCommand(viewModel, x => x.ActivateRunCommand)
                .DisposeWith(disposables);
            this.Events().SelectedCellChanged
                .Where(x => x.NewRow > -1 && x.NewRow < table.Rows.Count)
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
        }

        private RunInfoModel GetRunModel(int selectedRow)
        {
            return new()
            {
                RunId = (int)table.Rows[selectedRow][IdCol],
                Name = table.Rows[selectedRow][AlgorithmCol].ToString(),
                Visited = (int)table.Rows[selectedRow][VisitedCol],
                Steps = (int)table.Rows[selectedRow][StepsCol],
                Cost = (double)table.Rows[selectedRow][CostCol],
                Elapsed = (TimeSpan)table.Rows[selectedRow][ElapsedCol],
                StepRule = table.Rows[selectedRow][StepCol].ToString(),
                Heuristics = table.Rows[selectedRow][LogicCol].ToString(),
                Weight = table.Rows[selectedRow][WeightCol].ToString(),
                Status = table.Rows[selectedRow][StatusCol].ToString()
            };
        }

        private void OnAdded(RunInfoModel model)
        {
            table.Rows.Add(model.RunId, model.Name, model.Visited,
                model.Steps, model.Cost, model.Elapsed, model.StepRule,
                model.Heuristics, model.Weight, model.Status);
            table.AcceptChanges();
            SetNeedsDisplay();
            Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
        }

        private void OnRemoved(RunInfoModel model)
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
}
