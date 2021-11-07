using Common.ValueRanges;
using System;

namespace Common.Extensions
{
    public static class InclusiveValueRangeExtensions
    {
        private static readonly Random Random;

        static InclusiveValueRangeExtensions()
        {
            Random = new CryptoRandom();
        }

        public static int GetRandomValue(this InclusiveValueRange<int> valueRange)
        {
            if (valueRange.UpperValueOfRange == int.MaxValue)
            {
                return Random.Next(valueRange.LowerValueOfRange, valueRange.UpperValueOfRange);
            }
            else
            {
                return Random.Next(valueRange.LowerValueOfRange, valueRange.UpperValueOfRange + 1);
            }
        }

        public static int[] GetAllValuesInRange(this InclusiveValueRange<int> valueRange)
        {
            uint length = valueRange.Amplitude() + 1;
            var values = new int[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = valueRange.LowerValueOfRange + i;
            }
            return values;
        }

        public static uint Amplitude(this InclusiveValueRange<int> valueRange)
        {
            return (uint)(valueRange.UpperValueOfRange - valueRange.LowerValueOfRange);
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
