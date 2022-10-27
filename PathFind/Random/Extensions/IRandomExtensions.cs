using Random.Interface;
using System;
using ValueRange;
using ValueRange.Extensions;

namespace Random.Extensions
{
    public static class IRandomExtensions
    {
        public static int NextInt(this IRandom random, InclusiveValueRange<int> range)
        {
            long module = (long)range.Amplitude() + 1;
            return (int)(random.NextUint() % module) + range.LowerValueOfRange;
        }

        public static int NextInt(this IRandom random)
        {
            return random.NextInt(new InclusiveValueRange<int>(int.MaxValue));
        }

        public static int NextInt(this IRandom random, int minValue, int maxValue)
        {
            return random.NextInt(new InclusiveValueRange<int>(maxValue, minValue));
        }

        public static double NextDouble(this IRandom random, InclusiveValueRange<double> range)
        {
            return range.Amplitude() * ((double)random.NextInt() / int.MaxValue) + range.LowerValueOfRange;
        }

        public static TimeSpan NextTimeSpan(this IRandom random, InclusiveValueRange<TimeSpan> range)
        {
            double lower = range.LowerValueOfRange.TotalMilliseconds;
            double upper = range.UpperValueOfRange.TotalMilliseconds;
            var valueRange = new InclusiveValueRange<double>(lower, upper);
            double randomValue = random.NextDouble(valueRange);
            return TimeSpan.FromMilliseconds(randomValue);
        }
    }
}
