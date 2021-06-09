using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Common.ValueRanges
{
    /// <summary>
    /// Represents range of values (inclusively)
    /// </summary>
    [Serializable]
    public sealed class InclusiveValueRange : IValueRange
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
        public InclusiveValueRange(int upperValueOfRange, int lowerValueOfRange)
        {
            SwapIfLess(ref upperValueOfRange, ref lowerValueOfRange);

            UpperValueOfRange = upperValueOfRange;
            LowerValueOfRange = lowerValueOfRange;
        }

        static InclusiveValueRange()
        {
            Random = new Random();
        }

        public int UpperValueOfRange { get; }
        public int LowerValueOfRange { get; }

        private void SwapIfLess(ref int greaterValue, ref int lowerValue)
        {
            if (Comparer<int>.Default.Compare(greaterValue, lowerValue) < 0)
            {
                var temp = greaterValue;
                greaterValue = lowerValue;
                lowerValue = temp;
            }
        }

        public int ReturnInRange(int value)
        {
            if (value > UpperValueOfRange)
                value = UpperValueOfRange;
            if (value < LowerValueOfRange)
                value = LowerValueOfRange;
            return value;
        }

        public bool Contains(int value)
        {
            return value <= UpperValueOfRange && value >= LowerValueOfRange;
        }

        public int GetRandomValueFromRange()
        {
            return Random.Next(LowerValueOfRange, UpperValueOfRange + 1);
        }

        private static readonly Random Random;
    }
}
