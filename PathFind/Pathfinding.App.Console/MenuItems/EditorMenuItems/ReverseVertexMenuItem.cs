using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [MediumPriority]
    internal sealed class ReverseVertexMenuItem : SwitchVerticesMenuItem
    {
        public ReverseVertexMenuItem(IReadOnlyCollection<(string, IVertexAction)> actions,
            IMessenger messenger,
            IInput<ConsoleKey> keyInput)
            : base(actions, keyInput, messenger)
        {

        }

        public override string ToString()
        {
            return Languages.ReverseVertices;
        }
    }
}
