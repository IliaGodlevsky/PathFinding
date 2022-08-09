using Random.Interface;
using System;
using ValueRange;
using ValueRange.Extensions;

namespace Random.Extensions
{
    public static class IRandomExtensions
    {
        public static int Next(this IRandom self, InclusiveValueRange<int> range)
        {
            return self.Next(range.LowerValueOfRange, range.UpperValueOfRange);
        }

        public static int Next(this IRandom random)
        {
            return random.Next(default, int.MaxValue);
        }

        public static double Next(this IRandom random, InclusiveValueRange<double> range)
        {
            return range.Amplitude() * ((double)random.Next() / int.MaxValue) + range.LowerValueOfRange;
        }

        public static TimeSpan Next(this IRandom random, InclusiveValueRange<TimeSpan> range)
        {
            double lower = range.LowerValueOfRange.TotalMilliseconds;
            double upper = range.UpperValueOfRange.TotalMilliseconds;
            var valueRange = new InclusiveValueRange<double>(lower, upper);
            double randomValue = random.Next(valueRange);
            return TimeSpan.FromMilliseconds(randomValue);
        }
    }
}
