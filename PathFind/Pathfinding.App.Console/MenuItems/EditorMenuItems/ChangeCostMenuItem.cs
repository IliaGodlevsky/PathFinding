using GalaSoft.MvvmLight.Messaging;
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
        public ChangeCostMenuItem(IReadOnlyCollection<(string, IVertexAction)> actions,
            IInput<ConsoleKey> keyInput, IMessenger messenger)
            : base(actions, keyInput, messenger)
        {
        }

        public override string ToString()
        {
            return Languages.ChangeCost;
        }
    }
}
