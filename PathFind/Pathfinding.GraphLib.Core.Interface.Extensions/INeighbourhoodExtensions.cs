using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Interface.Extensions
{
    public static class INeighbourhoodExtensions
    {
        public static IEnumerable<IVertex> GetNeighboursWithinGraph<TVertex>(this INeighborhood self, IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            bool IsWithin(int coordinate, int graphDimension)
            {
                var range = new InclusiveValueRange<int>(graphDimension - 1);
                return range.Contains(coordinate);
            }
            bool IsWithinGraph(ICoordinate neighbour)
            {
                return neighbour.Juxtapose(graph.DimensionsSizes, IsWithin);
            }
            return self.Where(IsWithinGraph).Select(graph.Get).OfType<IVertex>();
        }
    }
}