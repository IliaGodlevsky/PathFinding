using Common.ValueRanges;
using System;
using System.Linq;

namespace Common.Extensions
{
    public static class InclusiveValueRangeExtensions
    {
        private static readonly Random Random;

        static InclusiveValueRangeExtensions()
        {
            Random = new Random();
        }

        public static int GetRandomValue(this InclusiveValueRange<int> valueRange)
        {
            return Random.Next(valueRange.LowerValueOfRange, valueRange.UpperValueOfRange + 1);
        }

        public static int[] GetAllValuesInRange(this InclusiveValueRange<int> valueRange)
        {
            int length = valueRange.Amplitude() + 1;
            return Enumerable.Range(valueRange.LowerValueOfRange, length).ToArray();
        }

        public static int Amplitude(this InclusiveValueRange<int> valueRange)
        {
            return valueRange.UpperValueOfRange - valueRange.LowerValueOfRange;
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
