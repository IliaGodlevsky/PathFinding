using Shared.Primitives.ValueRange;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class CostRangeMessage
    {
        public InclusiveValueRange<int> Range { get; }

        public CostRangeMessage(InclusiveValueRange<int> range)
        {
            Range = range;
        }
    }
}
