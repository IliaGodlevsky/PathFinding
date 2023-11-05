using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class DeleteGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly GraphsPathfindingHistory history;
        private readonly IMessenger messenger;
        private readonly IInput<int> input;

        private IGraph<Vertex> graph = Graph<Vertex>.Empty;

        public DeleteGraphMenuItem(IInput<int> input,
            GraphsPathfindingHistory history,
            IMessenger messenger)
        {
            this.history = history;
            this.messenger = messenger;
            this.input = input;
        }

        public bool CanBeExecuted()
        {
            return history.Count > 1;
        }

        public void Execute()
        {
            var graphs = history.Graphs.Except(graph).ToList();
            string menuList = GetMenuList(graphs);
            int index = GetIndex(menuList, graphs.Count);
            while (index != graphs.Count)
            {
                history.Remove(graphs[index]);
                graphs.RemoveAt(index);
                messenger.Send(new GraphDeletedMessage(graph), Tokens.Common);
                if (graphs.Count == 0)
                {
                    break;
                }
                menuList = GetMenuList(graphs);
                index = GetIndex(menuList, graphs.Count);
            }
        }

        private string GetMenuList(IReadOnlyCollection<IGraph<Vertex>> graphs)
        {
            return graphs.Select(s => s.ToString())
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

        private void SetGraph(IGraph<Vertex> graph)
        {
            this.graph = graph;
        }
    }
}
