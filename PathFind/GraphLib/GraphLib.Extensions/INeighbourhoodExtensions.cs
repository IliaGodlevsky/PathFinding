using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace GraphLib.Extensions
{
    public static class INeighbourhoodExtensions
    {
        public static IReadOnlyCollection<IVertex> GetNeighboursWithinGraph(this IEnumerable<ICoordinate> self, IGraph<IVertex> graph)
        {
            return self.GetNeighborsWithinGraphInternal(graph);
        }

        private static IEnumerable<ICoordinate> WithoutOutOfGraph(this IEnumerable<ICoordinate> self, IGraph<IVertex> graph)
        {
            return self.Where(neighbour =>
            {
                bool IsWithin(int coordinate, int graphDimension)
                {
                    var range = new InclusiveValueRange<int>(graphDimension - 1);
                    return range.Contains(coordinate);
                }
                return neighbour.Juxtapose(graph.DimensionsSizes, IsWithin);
            });
        }

        private static IReadOnlyCollection<IVertex> GetNeighborsWithinGraphInternal(this IEnumerable<ICoordinate> self, IGraph<IVertex> graph)
        {
            return self.WithoutOutOfGraph(graph)
                .Select(coordinate => graph.Get(coordinate))
                .ToReadOnly();
        }
    }
}