using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Shared.Extensions;
using Shared.Primitives;
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
        private readonly IService service;

        private bool isHistoryApplied = true;

        private GraphReadDto graph = GraphReadDto.Empty;

        public ShowHistoryMenuItem(IMessenger messenger,
            IInput<int> input,
            IService service)
        {
            this.service = service;
            this.input = input;
            this.messenger = messenger;
        }

        public bool CanBeExecuted()
        {
            var history = service.GetGraphPathfindingHistory(graph.Id);
            return IsHistoryApplied() && history.Count > 0;
        }

        public void Execute()
        {
            var grouped = service.GetGraphPathfindingHistory(graph.Id)
                .OrderBy(x => x.Statistics.Algorithm)
                .Select(x => (Id: x.Id, Statistics: x.Statistics))
                .GroupBy(x => x.Statistics.Algorithm);
            var ordered = grouped.SelectMany(x => x.OrderBy(x=>x.Statistics.AlgorithmSpeed)
                                                   .ThenBy(x => x.Statistics.Steps));
            var statistics = ordered.ToDictionary(x => x.Id, x => x.Statistics);
            string inputMessage = GetInputMessage(statistics.Values);
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
                            string data = $"{index + 1} {page.Value}";
                            var lineMsg = new StatisticsLineMessage(data);
                            messenger.Send(lineMsg, Tokens.AppLayout);
                            var keyMsg = new AlgorithmKeyMessage(page.Key);
                            messenger.Send(keyMsg, Tokens.History);
                        }
                    }
                    index = GetAlgorithmId(inputMessage, statistics.Count);
                }
            }
        }

        private string GetInputMessage(IReadOnlyCollection<Statistics> statistics)
        {
            var table = statistics.ToTable();
            string header = Aggregate(table.Headers);
            var rows = table.Rows.Select(Aggregate).ToArray();
            var menuList = rows.Append(Languages.Quit).CreateTable(header);
            return string.Concat(menuList, Languages.AlgorithmChoiceMsg);
        }

        private Disposable RememberGraphState()
        {
            var costs = graph.Graph.GetCosts();
            return Disposable.Use(() =>
            {
                using (Cursor.HideCursor())
                {
                    graph.Graph.ApplyCosts(costs);
                    messenger.Send(new ClearColorsMessage());
                }
            });
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }

        private int GetAlgorithmId(string message, int count)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return input.Input(message, count + 1, 1) - 1;
            }
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        private void SetIsApplied(IsAppliedMessage msg)
        {
            isHistoryApplied = msg.IsApplied;
        }

        public override string ToString()
        {
            return Languages.ShowHistory;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<ShowHistoryMenuItem, IsAppliedMessage>(this, Tokens.History, SetIsApplied);
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
