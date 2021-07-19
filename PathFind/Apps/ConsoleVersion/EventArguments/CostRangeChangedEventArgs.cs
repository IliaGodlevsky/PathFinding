using Common.ValueRanges;
using System;

namespace ConsoleVersion.EventArguments
{
    internal class CostRangeChangedEventArgs : EventArgs
    {
        public CostRangeChangedEventArgs(InclusiveValueRange<int> newValueRange)
        {
            NewValueRange = newValueRange;
        }

        public InclusiveValueRange<int> NewValueRange { get; }
    }
}
