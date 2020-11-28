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
            if (self.Coordinates.Count() != dimensions.Length + 1)
            {
                throw new ArgumentException("Not enough arguments");
            }

            var coordinates = self.Coordinates.ToArray();
            int index = coordinates.Last();

            for (int i = 0; i < dimensions.Length - 1; i++)
            {
                for (int j = 1 + i; j < dimensions.Length; j++)
                {
                    dimensions[i] *= dimensions[j];
                }
            }

            return dimensions.Zip(coordinates, (x, y) => x * y).Sum() + index;
        }

        internal static int ToIndex(this ICoordinate self, IGraph graph)
        {
            return self.ToIndex(graph.DimensionsSizes.Skip(1).ToArray());
        }

        internal static bool IsEqual(this ICoordinate self, ICoordinate coordinates)
        {
            if (self == null || coordinates == null)
            {
                return false;
            }

            return self.Coordinates.SequenceEqual(coordinates.Coordinates);
        }

        internal static bool IsWithinGraph(this ICoordinate coordinates, IGraph graph)
        {
            var dimensionComparer = new DimensionComparer();
            return coordinates.Coordinates.SequenceEqual(graph.DimensionsSizes, dimensionComparer);
        }
    }
}
