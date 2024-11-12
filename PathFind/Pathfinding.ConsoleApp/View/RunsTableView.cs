using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Shared.Extensions;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Collections.Specialized;
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

        public RunsTableView(IRunsTableViewModel viewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger) : this()
        {
            viewModel.Runs.CollectionChanged += OnCollectionChanged;
            this.Events().SelectedCellChanged
                .Where(x => x.NewRow > -1 && x.NewRow < table.Rows.Count)
                .Do(x => messenger.Send(new OpenAlgorithmRunViewMessage()))
                .Select(x => GetAllSelectedCells().DistinctBy(x => x.Y)
                      .Select(z => GetRunModel(z.Y)).ToArray())
                .InvokeCommand(viewModel, x => x.SelectRunsCommand)
                .DisposeWith(disposables);
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => x.MouseEvent.Y + RowOffset - headerLinesConsumed)
                .Where(x => x >= 0 && x < Table.Rows.Count && x == SelectedRow)
                .Do(x => messenger.Send(new OpenAlgorithmRunViewMessage()))
                .Select(x => GetRunModel(x).Enumerate().ToArray())
                .InvokeCommand(viewModel, x => x.SelectRunsCommand)
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
            Application.MainLoop.Invoke(() =>
            {
                table.Rows.Add(model.GetProperties());
                table.AcceptChanges();
                SetNeedsDisplay();
                SetCursorInvisible();
            });
        }

        private void OnRemoved(RunInfoModel model)
        {
            Application.MainLoop.Invoke(() =>
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
                    SetCursorInvisible();
                }
            });
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    MultiSelectedRegions.Clear();
                    table.Clear();
                    table.AcceptChanges();
                    SetNeedsDisplay();
                    break;
                case NotifyCollectionChangedAction.Add:
                    OnAdded((RunInfoModel)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    OnRemoved((RunInfoModel)e.OldItems[0]);
                    break;
            }
        }

        private static void SetCursorInvisible()
        {
            Application.Driver.SetCursorVisibility(CursorVisibility.Invisible);
        }
    }
}
