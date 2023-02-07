using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class ChangeCostMenuItem : SwitchVerticesMenuItem
    {
        public ChangeCostMenuItem(IMessenger messenger,
            IReadOnlyDictionary<ConsoleKey, IVertexAction> actions,
            IInput<ConsoleKey> keyInput)
            : base(messenger, actions, keyInput)
        {
        }

        public override string ToString()
        {
            return Languages.ChangeCost;
        }
    }
}
