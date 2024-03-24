using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Extensions;
using Shared.Primitives.Extensions;
using Pathfinding.App.Console.Localization;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class EnterNeighbourCycleModeMenuItem : GraphMenuItem
    {
        public EnterNeighbourCycleModeMenuItem(IMessenger messenger,
            IInput<int> input) : base(messenger, input)
        {
        }

        public override void Execute()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                var menuItem = ReturnOptions.Options.CreateMenuList(1).ToString();
                string message = $"{menuItem}{Languages.InputCycleMsg}";
                int cycleMode = input.Input(message, Constants.CycleModeValueRange) - 1;
                var msg = new ReturnOptionsMessage(ReturnOptions.Options[cycleMode]);
                messenger.Send(msg, Tokens.Graph);
            }
        }

        public override string ToString() => Languages.CycleMode;
    }
}
