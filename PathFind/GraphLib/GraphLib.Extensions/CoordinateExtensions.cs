using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace GraphLib.Extensions
{
    public static class CoordinateExtensions
    {
        public static bool IsEqual(this ICoordinate self, ICoordinate coordinate)
        {
            if (self == null || coordinate == null)
            {
                return false;
            }

            return self.CoordinatesValues.Juxtapose(coordinate.CoordinatesValues);
        }

        public static bool IsCardinal(this ICoordinate coordinate, ICoordinate neighbour)
        {
            return coordinate.CoordinatesValues.IsCardinal(neighbour.CoordinatesValues);
        }

        public static bool IsWithinGraph(this ICoordinate self, IGraph graph)
        {
            bool IsWithin(int coordinate, int graphDimension)
            {
                var range = new InclusiveValueRange<int>(graphDimension - 1);
                return range.Contains(coordinate);
            }

            return self.IsWithinGraph(graph, IsWithin);
        }

        public static bool IsWithinGraph(this ICoordinate self, IGraph graph, Func<int, int, bool> predicate)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            return self.CoordinatesValues.Juxtapose(graph.DimensionsSizes, predicate);
        }
    }
}
