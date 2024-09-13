using Pathfinding.Shared.Interface;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Shared.Extensions
{
    public static class RandomExtensions
    {
        /// <summary>
        /// Generates a random number that lays within the <paramref name="range"/>
        /// </summary>
        /// <param name="random"></param>
        /// <param name="range">A range within which a number should be generated</param>
        /// <returns>A random int within <paramref name="range"/></returns>
        public static int NextInt(this IRandom random, InclusiveValueRange<int> range)
        {
            return (int)(random.NextUInt() % (range.Amplitude() + 1)) + range.LowerValueOfRange;
        }

        /// <summary>
        /// Generates a random number that lays within 
        /// the range between <paramref name="minValue"/>
        /// and <paramref name="minValue"/>
        /// </summary>
        /// <param name="random"></param>
        /// <returns>A random int within the range 
        /// between <paramref name="minValue"/> 
        /// and <paramref name="maxValue"/></returns>
        public static int NextInt(this IRandom random, int maxValue = int.MaxValue, int minValue = default)
        {
            return random.NextInt(new(maxValue, minValue));
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
            return range.Amplitude() * ((double)random.NextUInt() / uint.MaxValue) + range.LowerValueOfRange;
        }

        public static IEnumerable<int> GenerateNumbers(this IRandom random, InclusiveValueRange<int> range, int limit)
        {
            while (limit-- > 0)
            {
                yield return random.NextInt(range);
            }
        }

        public static IEnumerable<Coordinate> GenerateCoordinates(this IRandom random,
            InclusiveValueRange<int> range, int dimensions, int limit)
        {
            while (limit-- > 0)
            {
                int dims = dimensions;
                var coordinateValues = new int[dimensions];
                while (dims-- > 0)
                {
                    coordinateValues[dims] = random.NextInt(range);
                }
                yield return new Coordinate(coordinateValues);
            }
        }

        public static Coordinate GenerateCoordinate(this IRandom random,
            InclusiveValueRange<int> range, int dimensions)
        {
            int dims = dimensions;
            var coordinateValues = new int[dimensions];
            while (dims-- > 0)
            {
                coordinateValues[dims] = random.NextInt(range);
            }
            return new Coordinate(coordinateValues);
        }
    }
}
