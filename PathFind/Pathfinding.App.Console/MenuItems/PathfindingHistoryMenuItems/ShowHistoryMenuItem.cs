using GalaSoft.MvvmLight.Messaging;
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
using Shared.Extensions;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    [HighPriority]
    internal sealed class ShowHistoryMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IInput<int> input;
        private readonly GraphsPathfindingHistory history;

        private bool isHistoryApplied = true;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public ShowHistoryMenuItem(IMessenger messenger,
            IInput<int> input,
            GraphsPathfindingHistory history)
        {
            this.history = history;
            this.input = input;
            this.messenger = messenger;
        }

        public bool CanBeExecuted()
        {
            return IsHistoryApplied() 
                && history.GetFor(graph).Algorithms.Count > 0;
        }

        public void Execute()
        {
            var statistics = history.GetFor(graph).Statistics
                .GroupBy(s => s.Value.Algorithm)
                .SelectMany(s => s.OrderBy(i => i.Value.Steps))
                .ToDictionary();
            var table = new Table<Statistics>(statistics.Values);
            string header = Aggregate(table.Headers);
            var rows = table.Rows.Select(Aggregate).ToArray();
            var menuList = rows.Append(Languages.Quit).CreateTable(header);
            string inputMessage = string.Concat(menuList, Languages.AlgorithmChoiceMsg);
            using (RememberGraphState())
            {
                int index = GetAlgorithmId(inputMessage, statistics.Count);
                while (index != statistics.Count)
                {
                    var page = statistics.ElementAt(index);
                    using (Cursor.UseCurrentPosition())
                    {
                        using (Cursor.HideCursor())
                        {
                            messenger.SendData(page.Key, Tokens.History);
                            string data = $"{index + 1} {page.Value}";
                            messenger.SendData(data, Tokens.AppLayout);
                        }
                    }
                    index = GetAlgorithmId(inputMessage, statistics.Count);
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

        private int GetAlgorithmId(string message, int count)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return input.Input(message, count + 1, 1) - 1;
            }
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        private void SetIsApplied(bool isApplied)
        {
            isHistoryApplied = isApplied;
        }

        public override string ToString()
        {
            return Languages.ShowHistory;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Bind(IsHistoryApplied, Tokens.History);
            messenger.RegisterData<bool>(this, Tokens.History, SetIsApplied);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        private static string Aggregate(IEnumerable<string> values)
        {
            var builder = new StringBuilder();
            foreach (var value in values)
            {
                builder.Append(value);
            }
            return builder.ToString();
        }
    }
}
