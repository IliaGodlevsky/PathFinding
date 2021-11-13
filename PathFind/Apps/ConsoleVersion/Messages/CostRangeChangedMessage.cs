using ValueRange;

namespace ConsoleVersion.Messages
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
