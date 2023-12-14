using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.Services;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class DeleteGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IService service;
        private readonly IMessenger messenger;
        private readonly IInput<int> input;

        private int id;
        private IGraph<Vertex> graph = Graph<Vertex>.Empty;

        public DeleteGraphMenuItem(IInput<int> input,
            IService service,
            IMessenger messenger)
        {
            this.service = service;
            this.messenger = messenger;
            this.input = input;
        }

        public bool CanBeExecuted()
        {
            return service.GetAllGraphInfo()
                .Where(x => x.Id != id).Any();
        }

        public void Execute()
        {
            var graphs = service.GetAllGraphInfo()
                .Where(x => x.Id != id).ToList();
            string menuList = GetMenuList(graphs);
            int index = GetIndex(menuList, graphs.Count);
            while (index != graphs.Count)
            {
                int id = graphs[index].Id;
                service.DeleteGraph(id);
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
        }

        private string GetMenuList(IReadOnlyCollection<GraphEntity> graphs)
        {
            return graphs.Select(s => s.ConvertToString())
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
        }

        public override string ToString()
        {
            return Languages.DeleteGraph;
        }

        public void RegisterHanlders(IMessenger messenger)
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
            id = msg.Id;
        }
    }
}
