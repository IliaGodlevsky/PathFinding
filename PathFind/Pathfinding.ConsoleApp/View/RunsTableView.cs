using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
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
            this.Events().KeyPress
                .Where(x => x.KeyEvent.Key.HasFlag(Key.A)
                    && x.KeyEvent.Key.HasFlag(Key.CtrlMask))
                .Throttle(TimeSpan.FromMilliseconds(150))
                .Select(x => MultiSelectedRegions
                        .SelectMany(x => (x.Rect.Top, x.Rect.Bottom - 1).Iterate())
                        .Select(GetRunModel)
                        .ToArray())
                .InvokeCommand(viewModel, x => x.SelectRunsCommand)
                .DisposeWith(disposables);
            this.Events().SelectedCellChanged
                .Where(x => x.NewRow > -1 && x.NewRow < Table.Rows.Count)
                .Do(x => messenger.Send(new OpenAlgorithmRunViewMessage()))
                .Select(x => GetSelectedRows())
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
            this.Events().KeyPress
                .Where(args => args.KeyEvent.Key.HasFlag(Key.R)
                    && args.KeyEvent.Key.HasFlag(Key.CtrlMask)
                    && Table.Rows.Count > 1)
                .Do(x => OrderTable(IdCol, Ascending))
                .Do(x => PreviousSortedColumn = string.Empty)
                .Select(x => GetSelectedRows())
                .InvokeCommand(viewModel, x => x.SelectRunsCommand)
                .DisposeWith(disposables);
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked
                    && Table.Rows.Count > 1
                    && x.MouseEvent.Y < headerLinesConsumed)
                .Do(OrderOnMouseClick)
                .Select(x => GetSelectedRows())
                .InvokeCommand(viewModel, x => x.SelectRunsCommand)
                .DisposeWith(disposables);
        }
        
        private RunInfoModel[] GetSelectedRows()
        {
            return GetAllSelectedCells().Select(x => x.Y)
                    .Distinct().Select(GetRunModel).ToArray();
        }

        private void OrderOnMouseClick(MouseEventArgs args)
        {
            var selectedColumn = ScreenToCell(args.MouseEvent.X,
                headerLinesConsumed);
            var column = Table.Columns[selectedColumn.Value.X].ColumnName;
            Order = PreviousSortedColumn != column || !Order;
            PreviousSortedColumn = column;
            string sortOrder = Order ? Ascending : Descending;
            OrderTable(column, sortOrder);
        }

        private void OrderTable(string columnName, string order)
        {
            Table.DefaultView.Sort = $"{columnName} {order}";
            Table = Table.DefaultView.ToTable();
            SetTableStyle();
            Table.AcceptChanges();
            SetNeedsDisplay();
        }

        private RunInfoModel GetRunModel(int selectedRow)
        {
            return new()
            {
                RunId = (int)Table.Rows[selectedRow][IdCol],
                Name = (Algorithms)Table.Rows[selectedRow][AlgorithmCol],
                Visited = (int)Table.Rows[selectedRow][VisitedCol],
                Steps = (int)Table.Rows[selectedRow][StepsCol],
                Cost = (double)Table.Rows[selectedRow][CostCol],
                Elapsed = (TimeSpan)Table.Rows[selectedRow][ElapsedCol],
                StepRule = FromTableValue<StepRules>(Table.Rows[selectedRow][StepCol]),
                Heuristics = FromTableValue<HeuristicFunctions>(Table.Rows[selectedRow][LogicCol]),
                Weight = FromTableValue<double>(Table.Rows[selectedRow][WeightCol]),
                Status = (RunStatuses)Table.Rows[selectedRow][StatusCol]
            };
        }

        private T? FromTableValue<T>(object value)
            where T : struct => value is DBNull ? null : (T?)value;

        private object ToTableValue<T>(T? value)
            where T : struct => value == null ? DBNull.Value : value.Value;

        private void OnAdded(RunInfoModel model)
        {
            Application.MainLoop.Invoke(() =>
            {
                Table.Rows.Add(model.RunId, 
                    model.Name, model.Visited,
                    model.Steps, 
                    model.Cost, model.Elapsed, 
                    ToTableValue(model.StepRule),
                    ToTableValue(model.Heuristics), 
                    ToTableValue(model.Weight), 
                    model.Status);
                Table.AcceptChanges();
                SetNeedsDisplay();
                SetCursorInvisible();
            });
        }

        private void OnRemoved(RunInfoModel model)
        {
            Application.MainLoop.Invoke(() =>
            {
                var row = Table.Rows.Find(model.RunId);
                var index = Table.Rows.IndexOf(row);
                if (row != null)
                {
                    row.Delete();
                    Table.AcceptChanges();
                    MultiSelectedRegions.Clear();
                    if (Table.Rows.Count > 0)
                    {
                        SelectedCellChangedEventArgs args = index == Table.Rows.Count
                            ? new(Table, 0, 0, index, index - 1)
                            : new(Table, 0, 0, index, index);
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
                    Table.Clear();
                    Table.AcceptChanges();
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
