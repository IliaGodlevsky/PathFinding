using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;

namespace Shared.Random.Extensions
{
    public static class IRandomExtensions
    {
        /// <summary>
        /// Generates a random number that lays within the <paramref name="range"/>
        /// </summary>
        /// <param name="random"></param>
        /// <param name="range">A range within which a number should be generated</param>
        /// <returns>A random int within <paramref name="range"/></returns>
        public static int NextInt(this IRandom random, InclusiveValueRange<int> range)
        {
            return (int)(random.NextUint() % (range.Amplitude() + 1)) + range.LowerValueOfRange;
        }

        /// <summary>
        /// Generates a random number that lays within 
        /// the range between 0 and <see cref="int.MaxValue"/>
        /// </summary>
        /// <param name="random"></param>
        /// <returns>A random int within the range 
        /// between 0 and <see cref="int.MaxValue"/></returns>
        public static int NextInt(this IRandom random)
        {
            return random.NextInt(new InclusiveValueRange<int>(int.MaxValue));
        }

        /// <summary>
        /// Generates a random double within the <paramref name="range"/>
        /// </summary>
        /// <param name="random"></param>
        /// <param name="range">A range within which a 
        /// random number should be generated</param>
        /// <returns>A random number within 
        /// the <paramref name="range"/></returns>
        public static double NextDouble(this IRandom random, InclusiveValueRange<double> range)
        {
            return range.Amplitude() * ((double)random.NextUint() / uint.MaxValue) + range.LowerValueOfRange;
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
