using Random.Interface;
using System.Runtime.CompilerServices;
using ValueRange;

namespace Random.Extensions
{
    public static class IRandomExtensions
    {
        /// <summary>
        /// Returns random number within the <paramref name="range"/> inclusivly
        /// </summary>
        /// <param name="self"></param>
        /// <param name="range">A range, that sets the boundaries 
        /// of the range of random values</param>
        /// <returns>A random number within <paramref name="range"/></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static int Next(this IRandom self, InclusiveValueRange<int> range)
        {
            return self.Next(range.LowerValueOfRange, range.UpperValueOfRange);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static int Next(this IRandom random)
        {
            return random.Next(default, int.MaxValue);
        }
    }
}
