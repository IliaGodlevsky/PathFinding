using Common.Extensions;
using System;
using System.Diagnostics;

namespace Shared.InclusiveValueRange
{
    [DebuggerDisplay("[{LowerValueOfRange}...{UpperValueOfRange}]")]
    public readonly struct InclusiveValueRange<T> : IEquatable<InclusiveValueRange<T>>
        where T : IComparable, IComparable<T>
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

        public override bool Equals(object obj)
        {
            return obj is InclusiveValueRange<T> range ? Equals(range) : false;
        }

        public bool Equals(InclusiveValueRange<T> other)
        {
            return other.UpperValueOfRange.IsEqual(UpperValueOfRange)
                && other.LowerValueOfRange.IsEqual(LowerValueOfRange);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UpperValueOfRange, LowerValueOfRange);
        }

        public override string ToString()
        {
            return $"[{LowerValueOfRange}...{UpperValueOfRange}]";
        }
    }
}
