using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Shared.Primitives.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class EnterNeighbourCycleModeMenuItem : GraphMenuItem
    {
        public EnterNeighbourCycleModeMenuItem(IMessenger messenger,
            IInput<int> input) : base(messenger, input)
        {
        }

        public override async Task ExecuteAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;
            using (Cursor.UseCurrentPositionWithClean())
            {
                var menuItem = ReturnOptions.Options.CreateMenuList(1).ToString();
                string message = $"{menuItem}{Languages.InputCycleMsg}";
                int cycleMode = input.Input(message, Constants.CycleModeValueRange) - 1;
                var msg = new ReturnOptionsMessage(ReturnOptions.Options[cycleMode]);
                messenger.Send(msg, Tokens.Graph);
                await Task.CompletedTask;
            }
        }

        public override string ToString() => Languages.CycleMode;
    }
}
