using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [HighPriority]
    internal sealed class ReverseVertexMenuItem : SwitchVerticesMenuItem
    {
        public ReverseVertexMenuItem(VertexActions actions,
            IInput<ConsoleKey> keyInput)
            : base(actions, keyInput)
        {

        }

        public override string ToString()
        {
            return Languages.ReverseVertices;
        }
    }
}
