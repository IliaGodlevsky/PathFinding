using Common.Extensions;
using System;
using System.Diagnostics;

namespace ValueRange
{
    [Serializable]
    [DebuggerDisplay("[{LowerValueOfRange} - {UpperValueOfRange}]")]
    public readonly struct InclusiveValueRange<T> where T : IComparable
    {
        public T UpperValueOfRange { get; }

        public T LowerValueOfRange { get; }

        public InclusiveValueRange(T upperValueOfRange, T lowerValueOfRange = default)
        {
            if (upperValueOfRange.IsLess(lowerValueOfRange))
            {
                UpperValueOfRange = lowerValueOfRange;
                LowerValueOfRange = upperValueOfRange;
            }
            else
            {
                UpperValueOfRange = upperValueOfRange;
                LowerValueOfRange = lowerValueOfRange;
            }
        }
    }
}
