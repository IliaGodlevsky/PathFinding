using Shared.Primitives.ValueRange;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class CostRangeChangedMessage(InclusiveValueRange<int> costRange)
    {
        public InclusiveValueRange<int> CostRange { get; } = costRange;
    }
}
