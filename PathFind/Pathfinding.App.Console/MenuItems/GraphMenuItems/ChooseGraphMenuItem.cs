using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [HighPriority]
    internal sealed class ChooseGraphMenuItem : IConditionedMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IInput<int> input;
        private readonly PathfindingHistory history;
        private readonly IPathfindingRangeBuilder<Vertex> builder;

        public ChooseGraphMenuItem(IMessenger messenger,
            IPathfindingRangeBuilder<Vertex> builder,
            IInput<int> input,
            PathfindingHistory history)
        {
            this.messenger = messenger;
            this.input = input;
            this.history = history;
            this.builder = builder;
        }

        public bool CanBeExecuted()
        {
            return history.Count > 1;
        }

        public void Execute()
        {
            var graphs = history.Graphs.ToList();
            var menuList = graphs.Select(s => s.ToString())
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
            int index = GetIndex(menuList, graphs.Count);
            if (index != graphs.Count)
            {
                var graph = graphs[index];
                var costRange = graph.First().Cost.CostRange;
                messenger.SendData(graph, Tokens.AppLayout, Tokens.Main, Tokens.Common);
                messenger.SendData(costRange, Tokens.AppLayout);
                var pathfindingRange = history.GetFor(graph).PathfindingRange;
                builder.Undo();
                builder.Include(pathfindingRange, graph);
            }
        }

        public override string ToString()
        {
            return Languages.ChooseGraph;
        }

        private int GetIndex(string message, int count)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = input.Input(message, count + 1, 1) - 1;
                return index;
            }
        }
    }
}
