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

        public static double NextDouble(this IRandom random, InclusiveValueRange<double> range)
        {
            return range.Amplitude() * ((double)random.Next() / int.MaxValue) + range.LowerValueOfRange;
        }

        public static TimeSpan NextTimeSpan(this IRandom random, InclusiveValueRange<TimeSpan> range)
        {
            var valueRange = new InclusiveValueRange<double>(range.LowerValueOfRange.TotalMilliseconds, 
                range.UpperValueOfRange.TotalMilliseconds);
            return TimeSpan.FromMilliseconds(random.NextDouble(valueRange));
        }
    }
}
