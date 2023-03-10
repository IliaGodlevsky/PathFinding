using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Shared.Primitives.ValueRange;

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
                var range = Constants.VerticesCostRange;
                int upperValueOfRange = input.Input(Languages.RangeUpperValueInputMsg, range);
                int lowerValueOfRange = input.Input(Languages.RangeLowerValueInputMsg, range);
                var costRange = new InclusiveValueRange<int>(upperValueOfRange, lowerValueOfRange);
                messenger.SendData(costRange, Tokens.Graph);
            }
        }

        public override string ToString()
        {
            return Languages.InputCostRange;
        }
    }
}
