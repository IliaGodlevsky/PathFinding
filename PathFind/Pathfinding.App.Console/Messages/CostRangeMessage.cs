using Shared.Primitives.ValueRange;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class CostRangeMessage
    {
        public InclusiveValueRange<int> CostRange { get; }

        public CostRangeMessage(InclusiveValueRange<int> costRange)
        {
            CostRange = costRange;
        }
    }
}
