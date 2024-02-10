using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class DeleteGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IService service;
        private readonly IMessenger messenger;
        private readonly IInput<int> input;

        private GraphReadDto graph = GraphReadDto.Empty;

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
                .Where(x => x.Id != graph.Id).Any();
        }

        public async void Execute()
        {
            var graphs = service.GetAllGraphInfo()
                .Where(x => x.Id != graph.Id).ToList();
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
            await Task.Run(() => ids.ForEach(id => service.DeleteGraph(id)));
        }

        private string GetMenuList(IReadOnlyCollection<GraphEntity> graphs)
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
        }
    }
}
