using Common.ValueRanges;
using System;

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
