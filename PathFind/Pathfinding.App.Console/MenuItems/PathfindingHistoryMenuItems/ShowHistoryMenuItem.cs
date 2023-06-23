using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Primitives;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    [HighPriority]
    internal sealed class ShowHistoryMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IInput<int> input;
        private readonly PathfindingHistory history;

        private bool isHistoryApplied = true;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public ShowHistoryMenuItem(IMessenger messenger, 
            IInput<int> input,
            PathfindingHistory history)
        {
            this.history = history;
            this.input = input;
            this.messenger = messenger;
        }

        public bool CanBeExecuted() => IsHistoryApplied();

        public void Execute()
        {
            var current = history.History[graph];
            var keys = current.Algorithms;
            var menuList = keys.Select(k => current.Statistics[k])
                .Select(note => note.ToString())
                .Append(Languages.Quit)
                .CreateMenuList(columnsNumber: 1);
            string inputMessage = string.Concat(menuList, "\n", Languages.AlgorithmChoiceMsg);
            using (RememberGraphState())
            {
                Guid id = GetAlgorithmId(inputMessage, keys.Count);
                while (id != Guid.Empty)
                {
                    var page = current.Statistics[id];
                    using (Cursor.UseCurrentPosition())
                    {
                        using (Cursor.HideCursor())
                        {
                            messenger.SendData(id, Tokens.History);
                            messenger.SendData(page.ToString(), Tokens.AppLayout);
                        }
                    }
                    id = GetAlgorithmId(inputMessage, keys.Count);
                }
            }
        }

        private Disposable RememberGraphState()
        {
            var costs = graph.GetCosts();
            return Disposable.Use(() =>
            {
                using (Cursor.HideCursor())
                {
                    graph.ApplyCosts(costs);
                    messenger.Send(new ClearColorsMessage());
                }
            });
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
        }

        private Guid GetAlgorithmId(string message, int count)
        {
            var current = history.History[graph];
            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = input.Input(message, count + 1, 1) - 1;
                return index == current.Algorithms.Count ? Guid.Empty : current.Algorithms[index];
            }
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        private void SetStatistics((PathfindingProcess Process, StatisticsNote Note) value)
        {
            history.History[graph].Statistics.Add(value.Process.Id, value.Note);
        }

        private void SetIsApplied(bool isApplied)
        {
            isHistoryApplied = isApplied;
        }

        public override string ToString()
        {
            return Languages.ShowHistory;
        }

        private void ClearStatistics(ClearHistoryMessage msg)
        {
            history.History[graph].Statistics.Clear();
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Bind(IsHistoryApplied, Tokens.History);
            messenger.RegisterData<bool>(this, Tokens.History, SetIsApplied);
            messenger.RegisterAlgorithmData<StatisticsNote>(this, token, SetStatistics);
            messenger.Register<ClearHistoryMessage>(this, token, ClearStatistics);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }
    }
}
