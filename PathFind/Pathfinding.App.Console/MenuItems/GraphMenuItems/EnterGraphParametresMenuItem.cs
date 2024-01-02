using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [HighPriority]
    internal sealed class EnterGraphParametresMenuItem : GraphMenuItem
    {
        public EnterGraphParametresMenuItem(IMessenger messenger, IInput<int> input)
            : base(messenger, input)
        {

        }

        public override void Execute()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                int width = input.Input(Languages.GraphWidthInputMsg, Constants.GraphWidthValueRange);
                int length = input.Input(Languages.GraphHeightInputMsg, Constants.GraphLengthValueRange);
                var message = new GraphParamsMessage(width, length);
                messenger.Send(message, Tokens.Graph);
            }
        }

        public override string ToString()
        {
            return Languages.InputGraphParametres;
        }
    }
}
