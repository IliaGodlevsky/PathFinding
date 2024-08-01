using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Requests.Read;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class ChooseGraphMenuItem(IMessenger messenger,
        IPathfindingRangeBuilder<Vertex> builder,
        IInput<int> input,
        IRequestService<Vertex> service) : IConditionedMenuItem
    {
        private readonly IMessenger messenger = messenger;
        private readonly IInput<int> input = input;
        private readonly IRequestService<Vertex> service = service;
        private readonly IPathfindingRangeBuilder<Vertex> builder = builder;

        public bool CanBeExecuted()
        {
            return service.ReadGraphCountAsync().Result > 0;
        }

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            if (!token.IsCancellationRequested)
            {
                var graphs = await service.ReadAllGraphInfoAsync(token);
                string menu = graphs.GraphInformations.Select(k => k.ConvertToString())
                    .Append(Languages.Quit)
                    .CreateMenuList(1)
                    .ToString();
                string menuList = string.Concat(menu, "\n", Languages.MenuOptionChoiceMsg);
                int index = GetIndex(menuList, graphs.GraphInformations.Count);
                if (index != graphs.GraphInformations.Count)
                {
                    int id = graphs.GraphInformations[index].Id;
                    var graph = await service.ReadGraphAsync(new ReadGraphRequest(id), token);
                    var costRange = graph.Graph.First().Cost.CostRange;
                    messenger.SendMany(new GraphMessage(graph), Tokens.Visual,
                        Tokens.AppLayout, Tokens.Main, Tokens.Common);
                    messenger.Send(new CostRangeChangedMessage(costRange), Tokens.AppLayout);
                    var range = await service.ReadRangeAsync(id, token);
                    builder.Undo();
                    builder.Include(range.Range, graph.Graph);
                }
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
