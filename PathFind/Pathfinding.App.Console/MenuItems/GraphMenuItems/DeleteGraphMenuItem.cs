using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class DeleteGraphMenuItem(IInput<int> input,
        IMessenger messenger,
        IRequestService<Vertex> service) : IConditionedMenuItem, ICanReceiveMessage
    {
        private readonly IRequestService<Vertex> service = service;
        private readonly IMessenger messenger = messenger;
        private readonly IInput<int> input = input;

        private GraphModel<Vertex> graph = new() { Graph = Graph<Vertex>.Empty, Id = 0, Name = string.Empty };

        public bool CanBeExecuted()
        {
            return service.ReadAllGraphInfoAsync()
                .Result
                .GraphInformations
                .Where(x => x.Id != graph.Id)
                .Any();
        }

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            var graphs = (await service.ReadAllGraphInfoAsync(token))
                .GraphInformations
                .Where(x => x.Id != graph.Id)
                .ToList();
            string menuList = GetMenuList(graphs);
            int index = GetIndex(menuList, graphs.Count);
            var ids = new List<int>();
            while (index != graphs.Count)
            {
                int id = graphs[index].Id;
                ids.Add(id);
                graphs.RemoveAt(index);
                var message = new GraphDeletedMessage(id);
                messenger.Send(message, Tokens.Common);
                if (graphs.Count == 0)
                {
                    break;
                }
                menuList = GetMenuList(graphs);
                index = GetIndex(menuList, graphs.Count);
            }
            await service.DeleteGraphsAsync(ids, token);
        }

        private string GetMenuList(IReadOnlyCollection<GraphInformationModel> graphs)
        {
            string menu = graphs.Select(s => s.ConvertToString())
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
            return string.Concat(menu, "\n", Languages.MenuOptionChoiceMsg);
        }

        public override string ToString()
        {
            return Languages.DeleteGraph;
        }

        public void RegisterHandlers(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        private int GetIndex(string message, int count)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return input.Input(message, count + 1, 1) - 1;
            }
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }
    }
}
