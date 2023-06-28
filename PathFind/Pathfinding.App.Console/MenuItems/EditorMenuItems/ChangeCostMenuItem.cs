using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [MediumPriority]
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
