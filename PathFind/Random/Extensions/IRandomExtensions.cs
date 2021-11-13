using Random.Interface;
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
        public static int Next(this IRandom self, InclusiveValueRange<int> range)
        {
            return self.Next(range.UpperValueOfRange, range.LowerValueOfRange);
        }

        /// <summary>
        /// Returns a double value within the range of 0 to 1 inclusivly
        /// </summary>
        /// <param name="self"></param>
        /// <returns>A value within the range if 0 to 1 inclusivly </returns>
        public static double NextDouble(this IRandom self)
        {
            return self.Next() / (double)int.MaxValue;
        }

        /// <summary>
        /// Return a positive value within range of 0 to <see cref="int.MaxValue"/>
        /// </summary>
        /// <param name="self"></param>
        /// <returns>A positive number within the range 
        /// of 0 to <see cref="int.MaxValue"/> </returns>
        public static int Next(this IRandom self)
        {
            return self.Next(0, int.MaxValue);
        }


    }
}
