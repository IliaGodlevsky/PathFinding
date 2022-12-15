using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class EnterGraphParametresMenuItem : GraphMenuItem
    {
        public override int Order => 3;

        public EnterGraphParametresMenuItem(IMessenger messenger, IInput<int> input) 
            : base(messenger, input)
        {

        }

        public override void Execute()
        {
            using (Cursor.CleanUpAfter())
            {
                int width = input.Input(Languages.GraphWidthInputMsg, Constants.GraphWidthValueRange);
                int length = input.Input(Languages.GraphHeightInputMsg, Constants.GraphLengthValueRange);
                messenger.Send(new GraphParametresMessage(width, length));
            }
        }

        public override string ToString()
        {
            return Languages.InputGraphParametres;
        }
    }
}
