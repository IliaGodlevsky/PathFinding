﻿using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class EnterCostRangeMenuItem : GraphMenuItem
    {
        public EnterCostRangeMenuItem(IMessenger messenger, IInput<int> input)
            : base(messenger, input)
        {
        }

        public override void Execute()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                var costRange = input.InputRange(Constants.VerticesCostRange);
                messenger.Send(new CostRangeMessage(costRange));
            }
        }

        public override string ToString()
        {
            return Languages.InputCostRange;
        }
    }
}
