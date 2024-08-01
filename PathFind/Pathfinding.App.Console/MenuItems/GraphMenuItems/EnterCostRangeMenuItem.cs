using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Shared.Primitives.ValueRange;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class EnterCostRangeMenuItem : GraphMenuItem
    {
        public EnterCostRangeMenuItem(IMessenger messenger, IInput<int> input)
            : base(messenger, input)
        {
        }

        public override async Task ExecuteAsync(CancellationToken token = default)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                var range = Constants.VerticesCostRange;
                int upperValueOfRange = input.Input(Languages.RangeUpperValueInputMsg, range);
                int lowerValueOfRange = input.Input(Languages.RangeLowerValueInputMsg, range);
                var costRange = new InclusiveValueRange<int>(upperValueOfRange, lowerValueOfRange);
                messenger.Send(new CostRangeMessage(costRange), Tokens.Graph);
                await Task.CompletedTask;
            }
        }

        public override string ToString()
        {
            return Languages.InputCostRange;
        }
    }
}
