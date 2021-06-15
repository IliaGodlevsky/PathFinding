using System;
using System.Collections.Generic;

namespace Common.ValueRanges
{
    /// <summary>
    /// Represents range of values (inclusively)
    /// </summary>
    [Serializable]
    public sealed class InclusiveValueRange<T> : IValueRange<T>
        where T : IComparable, IComparable<T>
    {
        /// <summary>
        /// Creates a new instance of <see cref="InclusiveValueRange"/> 
        /// with <paramref name="upperValueOfRange"/> and 
        /// <paramref name="lowerValueOfRange"/> as extreme values of the range
        /// </summary>
        /// <param name="upperValueOfRange"></param>
        /// <param name="lowerValueOfRange"></param>
        /// <remarks>If <paramref name="upperValueOfRange"/> 
        /// is less than <paramref name="lowerValueOfRange"/>
        /// then this parameters will be swapped</remarks>
        public InclusiveValueRange(T upperValueOfRange, T lowerValueOfRange)
        {
            SwapIfLess(ref upperValueOfRange, ref lowerValueOfRange);

            UpperValueOfRange = upperValueOfRange;
            LowerValueOfRange = lowerValueOfRange;
        }

        public T UpperValueOfRange { get; }
        public T LowerValueOfRange { get; }

        private void SwapIfLess(ref T greaterValue, ref T lowerValue)
        {
            if (Comparer<T>.Default.Compare(greaterValue, lowerValue) < 0)
            {
                T temp = greaterValue;
                greaterValue = lowerValue;
                lowerValue = temp;
            }
        }

        public T ReturnInRange(T value)
        {
            if (value.CompareTo(UpperValueOfRange) == 1)
            {
                value = UpperValueOfRange;
            }
            if (value.CompareTo(LowerValueOfRange) == -1)
            {
                value = LowerValueOfRange;
            }
            return value;
        }

        public bool Contains(T value)
        {
            return value.CompareTo(UpperValueOfRange) <= 0
                && value.CompareTo(LowerValueOfRange) >= 0;
        }
    }
}
