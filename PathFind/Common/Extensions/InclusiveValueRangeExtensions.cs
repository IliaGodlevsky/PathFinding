using Common.ValueRanges;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class InclusiveValueRangeExtensions
    {
        private static readonly InclusiveRangeRandom Random;

        static InclusiveValueRangeExtensions()
        {
            Random = new InclusiveRangeRandom();
        }

        /// <summary>
        /// Return random number within the range
        /// </summary>
        /// <param name="valueRange"></param>
        /// <returns>A random number within the range</returns>
        public static int GetRandomValue(this InclusiveValueRange<int> valueRange)
        {
            return Random.Next(valueRange);
        }

        public static IEnumerable<int> GetAllValuesInRange(this InclusiveValueRange<int> valueRange)
        {
            for (int i = valueRange.LowerValueOfRange; i <= valueRange.UpperValueOfRange; i++)
            {
                yield return i;
            }
        }

        public static uint Amplitude(this InclusiveValueRange<int> valueRange)
        {
            return (uint)((long)valueRange.UpperValueOfRange - valueRange.LowerValueOfRange);
        }

        public static double Amplitude(this InclusiveValueRange<double> valueRange)
        {
            return valueRange.UpperValueOfRange - valueRange.LowerValueOfRange;
        }

        public static double GetRandomValue(this InclusiveValueRange<double> valueRange)
        {
            return valueRange.Amplitude() * Random.NextDouble() + valueRange.LowerValueOfRange;
        }
    }
}
