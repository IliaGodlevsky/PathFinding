using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [LowPriority]
    internal sealed class ChangeCostMenuItem : SwitchVerticesMenuItem
    {
        public ChangeCostMenuItem(VertexActions actions,
            IInput<ConsoleKey> keyInput)
            : base(actions, keyInput)
        {
        }

        public override string ToString()
        {
            return Languages.ChangeCost;
        }
    }
}
