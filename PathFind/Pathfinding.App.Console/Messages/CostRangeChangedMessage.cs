using Shared.Primitives.ValueRange;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class CostRangeChangedMessage
    {
        public InclusiveValueRange<int> CostRange { get; }

        public CostRangeChangedMessage(InclusiveValueRange<int> costRange)
        {
            CostRange = costRange;
        }
    }
}
