using Random.Interface;
using Random.Realizations;
using ValueRange;
using ValueRange.Extensions;

namespace Random.Extensions
{
    public static class InclusiveValueRangeExtensions
    {
        private static readonly IRandom Random;

        static InclusiveValueRangeExtensions()
        {
            Random = new InclusiveRangeCryptoRandom();
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

        public static double GetRandomValue(this InclusiveValueRange<double> valueRange)
        {
            return valueRange.Amplitude() * Random.NextDouble() + valueRange.LowerValueOfRange;
        }
    }
}