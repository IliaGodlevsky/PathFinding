using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Core.Abstractions;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [HighPriority]
    internal sealed class ChooseGraphMenuItem : IConditionedMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IInput<int> input;
        private readonly PathfindingHistory history;

        public ChooseGraphMenuItem(IMessenger messenger,
            IInput<int> input,
            PathfindingHistory history)
        {
            this.messenger = messenger;
            this.input = input;
            this.history = history;
        }

        public bool CanBeExecuted()
        {
            return history.History.Count > 0;
        }

        public void Execute()
        {
            var graphs = history.History.Keys.ToList();
            var menuList = graphs.Select(s => s.ToString())
                .Append(Languages.Quit)
                .CreateMenuList(1)
                .ToString();
            int index = GetIndex(menuList, graphs.Count);
            if (index != graphs.Count)
            {
                messenger.SendData(graphs[index], Tokens.AppLayout, Tokens.Main, Tokens.Common);
            }
        }

        public override string ToString()
        {
            return "Choose graph";
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
