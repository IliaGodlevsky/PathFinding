using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using System.Resources;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class EnterCostRangeMenuItem : GraphMenuItem
    {
        public override int Order => 5;

        public EnterCostRangeMenuItem(IMessenger messenger, IInput<int> input) 
            : base(messenger, input)
        {
        }

        public override void Execute()
        {
            using (Cursor.CleanUpAfter())
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
