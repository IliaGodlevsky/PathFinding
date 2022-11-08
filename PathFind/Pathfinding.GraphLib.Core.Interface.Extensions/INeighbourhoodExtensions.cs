using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Interface.Extensions
{
    public static class INeighbourhoodExtensions
    {
        public static IReadOnlyCollection<IVertex> GetNeighboursWithinGraph(this INeighborhood self, IGraph<IVertex> graph)
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