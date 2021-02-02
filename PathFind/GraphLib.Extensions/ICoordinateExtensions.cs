using Common.Extensions;
using GraphLib.Interface;
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
        /// coordinates values number is not equal to <paramref name="dimensions"/></exception>
        /// <exception cref="ArgumentNullException">Thrown when any of arguments is null</exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal static int ToIndex(this ICoordinate self, params int[] dimensions)
        {
            if (self == null || dimensions == null)
            {
                throw new ArgumentNullException("Argument can't be null");
            }

            if (self.CoordinatesValues.Count() != dimensions.Length)
            {
                throw new ArgumentException("Dimensions length must be " +
                    "equals to the number of coordinate values");
            }

            if (!self.CoordinatesValues.Match(dimensions, (a, b) => a < b))
            {
                throw new ArgumentOutOfRangeException("Coordinate is out of dimensions range");
            }

            return dimensions
                .Skip(1)
                .StepAggregate(Multiply)
                .Zip(self.CoordinatesValues, Multiply)
                .Sum() + self.CoordinatesValues.Last();
        }

        /// <summary>
        /// Converts coordinates values to index in a graph
        /// </summary>
        /// <param name="self"></param>
        /// <param name="graph"></param>
        /// <returns>Index of coordinate in a graph</returns>
        /// /// <exception cref="ArgumentException">Throws when <paramref name="self"/> 
        /// coordinates values number is not equal to <paramref name="graph"/> size</exception>
        public static int ToIndex(this ICoordinate self, IGraph graph)
        {
            return self.ToIndex(graph.DimensionsSizes.ToArray());
        }

        /// <summary>
        /// Compares two coordinates
        /// </summary>
        /// <param name="self"></param>
        /// <param name="coordinate"></param>
        /// <returns>true if all of the coordinates values of <paramref name="self"/> 
        /// equals to the corresponding coordinates of <paramref name="coordinate"/>;
        /// false if not, or if they have not equal number of coordinates values
        /// or any of parametres is null</returns>
        public static bool IsEqual(this ICoordinate self, ICoordinate coordinate)
        {
            if (self == null || coordinate == null)
            {
                return false;
            }

            return self.CoordinatesValues.SequenceEqual(coordinate.CoordinatesValues);
        }

        /// <summary>
        /// Checks whether coordinate is within graph
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="graph"></param>
        /// <returns>true if <paramref name="coordinate"/> coordinates 
        /// values is not greater than the corresponding dimension if graph 
        /// and is not lesser than 0; false if is or coordinate has more or 
        /// less coordinates values than <paramref name="graph"/> has dimensions</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of parametres is null</exception>
        public static bool IsWithinGraph(this ICoordinate coordinate, IGraph graph)
        {
            if (coordinate == null || graph == null)
            {
                throw new ArgumentNullException("Argument can't be null");
            }

            return coordinate.CoordinatesValues.Match(graph.DimensionsSizes, IsWithin);
        }

        private static bool IsWithin(int coordinate, int graphDimension)
        {
            return coordinate < graphDimension && coordinate >= 0;
        }
    }
}
