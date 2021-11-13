using Common.Extensions;
using System;

namespace ValueRange
{
    /// <summary>
    /// Represents inclusive range of values
    /// </summary>
    [Serializable]
    public readonly struct InclusiveValueRange<T> where T : struct, IComparable
    {
        public T UpperValueOfRange { get; }
        public T LowerValueOfRange { get; }

        /// <summary>
        /// Creates a new instance of <see cref="InclusiveValueRange{T}"/> 
        /// with <paramref name="upperValueOfRange"/> and 
        /// <paramref name="lowerValueOfRange"/> as extreme values of the range
        /// </summary>
        /// <param name="upperValueOfRange"></param>
        /// <param name="lowerValueOfRange"></param>
        /// <remarks>If <paramref name="upperValueOfRange"/> 
        /// is less than <paramref name="lowerValueOfRange"/>
        /// then this parameters will be swapped</remarks>
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

        public T ReturnInRange(T value)
        {
            if (value.IsGreater(UpperValueOfRange))
            {
                value = UpperValueOfRange;
            }
            if (value.IsLess(LowerValueOfRange))
            {
                value = LowerValueOfRange;
            }
            return value;
        }

        public bool Contains(T value)
        {
            return value.IsBetween(UpperValueOfRange, LowerValueOfRange);
        }
    }
}
