using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.DataAccess.Repo;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.Visualization.Extensions;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [HighPriority]
    internal sealed class ChooseGraphMenuItem : IConditionedMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IInput<int> input;
        private readonly IDbContextService service;
        private readonly IPathfindingRangeBuilder<Vertex> builder;

        public ChooseGraphMenuItem(IMessenger messenger,
            IPathfindingRangeBuilder<Vertex> builder,
            IInput<int> input,
            IDbContextService service)
        {
            this.messenger = messenger;
            this.input = input;
            this.service = service;
            this.builder = builder;
        }

        public bool CanBeExecuted()
        {
            return service.GetAllGraphsInfo().Count > 1;
        }

        public void Execute()
        {
            var graphs = service.GetAllGraphsInfo().ToArray();
            var names = graphs.Select(x =>
            {
                return $"Width: {x.Width}\t Length: {x.Length}\t Obstacles: {x.ObstaclesCount}";
            }).ToArray();
            int count = graphs.Length;
            var menuList = graphs.Select(s => s.ToString())
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
            int index = GetIndex(menuList, count);
            if (index != count)
            {
                int id = graphs[index].Id;
                var graph = service.GetGraph(id);
                var costRange = graph.First().Cost.CostRange;
                messenger.SendMany(new GraphMessage(graph), Tokens.Visual,
                    Tokens.AppLayout, Tokens.Main, Tokens.Common);
                messenger.Send(new CostRangeChangedMessage(costRange), Tokens.AppLayout);
                var pathfindingRange = service.GetPathfindingRange(id);
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
