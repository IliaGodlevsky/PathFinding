using GraphLib.Comparers;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Graphs.Abstractions;
using System;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class ICoordinateExtensions
    {
        private static int Multiply(int x, int y) => x * y;

        /// <summary>
        /// Converts coordinates values to index in a multidimensional array
        /// </summary>
        /// <param name="self"></param>
        /// <param name="dimensions">dimensions of array except first dimension</param>
        /// <returns>Index of coordinate in a multidimensional array</returns>
        /// <exception cref="ArgumentException">Throws when <paramref name="self"/> 
        /// coordinates values number is not equal to <paramref name="dimensions"/> -1</exception>
        public static int ToIndex(this ICoordinate self, params int[] dimensions)
        {
            if (self.CoordinatesValues.Count() != dimensions.Length + 1)
            {
                throw new ArgumentException("Dimensions length must be " +
                    "one less than the number of coordinate values");
            }

            return dimensions
                .StepAggregate(Multiply)
                .Zip(self.CoordinatesValues, Multiply)
                .Sum() 
                + self.CoordinatesValues.Last();
        }

        /// <summary>
        /// Converts coordinates values to index in a graph
        /// </summary>
        /// <param name="self"></param>
        /// <param name="graph"></param>
        /// <returns>Index of coordinate in a graph</returns>
        /// /// <exception cref="ArgumentException">Throws when <paramref name="self"/> 
        /// coordinates values number is not equal to <paramref name="graph"/> size - 1</exception>
        internal static int ToIndex(this ICoordinate self, IGraph graph)
        {
            return self.ToIndex(graph.DimensionsSizes.Skip(1).ToArray());
        }

        internal static bool IsEqual(this ICoordinate self, ICoordinate coordinate)
        {
            if (self == null || coordinate == null)
            {
                return false;
            }

            return self.CoordinatesValues.SequenceEqual(coordinate.CoordinatesValues);
        }

        internal static bool IsWithinGraph(this ICoordinate coordinate, IGraph graph)
        {
            var dimensionComparer = new DimensionComparer();
            return coordinate.CoordinatesValues.SequenceEqual(graph.DimensionsSizes, dimensionComparer);
        }
    }
}
