using Shared.Primitives.ValueRange;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class CostRangeChangedMessage
    {
        public InclusiveValueRange<int> Range { get; }

        public CostRangeChangedMessage(InclusiveValueRange<int> range)
        {
            Range = range;
        }
    }
}