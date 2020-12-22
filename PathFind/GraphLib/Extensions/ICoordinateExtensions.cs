using GraphLib.Comparers;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Graphs.Abstractions;
using System;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class ICoordinateExtensions
    {
        public static int ToIndex(this ICoordinate self, params int[] dimensions)
        {
            if (self.CoordinatesValues.Count() != dimensions.Length + 1)
            {
                throw new ArgumentException("Not enough arguments");
            }

            var coordinatesValues = self.CoordinatesValues.ToArray();
            int index = coordinatesValues.Last();

            for (int i = 0; i < dimensions.Length - 1; i++)
            {
                for (int j = 1 + i; j < dimensions.Length; j++)
                {
                    dimensions[i] *= dimensions[j];
                }
            }

            return dimensions.Zip(coordinatesValues, (x, y) => x * y).Sum() + index;
        }

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
