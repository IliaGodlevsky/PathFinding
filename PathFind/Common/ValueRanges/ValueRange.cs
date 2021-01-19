using System.Collections.Generic;

namespace Common.ValueRanges
{
    public class ValueRange
    {
        public int UpperRange { get; }
        public int LowerRange { get; }

        public ValueRange(int upperValueRange, int lowerValueRange)
        {
            SwapIf(ref upperValueRange, 
                ref lowerValueRange, Comparer<int>.Default);

            UpperRange = upperValueRange;
            LowerRange = lowerValueRange;
        }

        public int ReturnInRange(int value)
        {
            if (value > UpperRange)
            {
                value = LowerRange;
            }
            else if (value < LowerRange)
            {
                value = UpperRange;
            }

            return value;
        }

        /// <summary>
        /// Checks whether values is in range of value inclusively
        /// </summary>
        /// <param name="value"></param>
        /// <returns>true if <paramref name="value"/> 
        /// is in range inclusively and false if not</returns>
        public bool IsInRage(int value)
        {
            return value <= UpperRange && value >= LowerRange;
        }

        public static void SwapIf<TSource>(ref TSource greaterValue, 
            ref TSource lowerValue, IComparer<TSource> comparer)
        {
            if (comparer.Compare(greaterValue, lowerValue) < 0)
            {
                var temp = greaterValue;
                greaterValue = lowerValue;
                lowerValue = temp;
            }
        }
    }
}
