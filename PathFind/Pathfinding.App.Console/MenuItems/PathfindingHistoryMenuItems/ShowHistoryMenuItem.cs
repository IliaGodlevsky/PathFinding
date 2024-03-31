using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Shared.Extensions;
using Shared.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    [HighPriority]
    internal sealed class ShowHistoryMenuItem : IConditionedMenuItem, ICanReceiveMessage
    {
        private readonly IMessenger messenger;
        private readonly IInput<int> input;
        private readonly IService<Vertex> service;

        private bool isHistoryApplied = true;

        private GraphReadDto<Vertex> graph = GraphReadDto<Vertex>.Empty;

        public ShowHistoryMenuItem(IMessenger messenger,
            IInput<int> input,
            IService<Vertex> service)
        {
            this.service = service;
            this.input = input;
            this.messenger = messenger;
        }

        public bool CanBeExecuted()
        {
            return IsHistoryApplied() && service.GetRunCount(graph.Id) > 0;
        }

        public void Execute()
        {
            var statistics = service.GetRunStatisticsForGraph(graph.Id)
                .OrderBy(x => x.AlgorithmId)
                .GroupBy(x => x.AlgorithmId)
                .SelectMany(x => x.OrderBy(x => x.AlgorithmSpeed).ThenBy(x => x.Steps))
                .ToReadOnly();
            string inputMessage = GetInputMessage(statistics);
            using (RememberGraphState())
            {
                int index = GetAlgorithmId(inputMessage, statistics.Count);
                while (index != statistics.Count)
                {
                    var page = statistics[index];
                    using (Cursor.UseCurrentPosition())
                    {
                        using (Cursor.HideCursor())
                        {
                            string data = $"{index + 1} {page}";
                            var lineMsg = new StatisticsLineMessage(data);
                            messenger.Send(lineMsg, Tokens.AppLayout);
                            var keyMsg = new AlgorithmKeyMessage(page.AlgorithmRunId);
                            messenger.Send(keyMsg, Tokens.History);
                        }
                    }
                    index = GetAlgorithmId(inputMessage, statistics.Count);
                }
            }
        }

        private string GetInputMessage(IReadOnlyCollection<RunStatisticsDto> statistics)
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

        public void RegisterHandlers(IMessenger messenger)
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
