using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class ChooseGraphMenuItem(IMessenger messenger,
        IPathfindingRangeBuilder<Vertex> builder,
        IInput<int> input,
        IService<Vertex> service) : IConditionedMenuItem
    {
        private readonly IMessenger messenger = messenger;
        private readonly IInput<int> input = input;
        private readonly IService<Vertex> service = service;
        private readonly IPathfindingRangeBuilder<Vertex> builder = builder;

        public bool CanBeExecuted()
        {
            return service.GetGraphCount() > 0;
        }

        public void Execute()
        {
            var graphs = service.GetAllGraphInfo();
            string menu = graphs.Select(k => k.ConvertToString())
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
            string menuList = string.Concat(menu, "\n", Languages.MenuOptionChoiceMsg);
            int index = GetIndex(menuList, graphs.Count);
            if (index != graphs.Count)
            {
                int id = graphs[index].Id;
                var graph = service.GetGraph(id);
                var costRange = graph.Graph.First().Cost.CostRange;
                messenger.SendMany(new GraphMessage(graph), Tokens.Visual,
                    Tokens.AppLayout, Tokens.Main, Tokens.Common);
                messenger.Send(new CostRangeChangedMessage(costRange), Tokens.AppLayout);
                var range = service.GetRange(id);
                builder.Undo();
                builder.Include(range, graph.Graph);
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
