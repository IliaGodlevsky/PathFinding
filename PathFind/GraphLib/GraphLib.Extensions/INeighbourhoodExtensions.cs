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
            bool IsWithin(int coordinate, int graphDimension)
            {
                var range = new InclusiveValueRange<int>(graphDimension - 1);
                return range.Contains(coordinate);
            }
            return self.Where(neighbour => neighbour.Juxtapose(graph.DimensionsSizes, IsWithin))
                .Select(graph.Get)
                .ToReadOnly();
        }
    }
}