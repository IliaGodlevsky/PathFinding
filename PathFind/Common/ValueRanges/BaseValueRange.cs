using System;
using System.Collections.Generic;

namespace Common.ValueRanges
{
    [Serializable]
    public abstract class BaseValueRange
    {
        protected BaseValueRange(int upperValueOfRange, int lowerValueOfRange)
        {
            SwapIfLess(ref upperValueOfRange, ref lowerValueOfRange);

            UpperValueOfRange = upperValueOfRange;
            LowerValueOfRange = lowerValueOfRange;
        }

        static BaseValueRange()
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

        protected static readonly Random Random;
    }
}
