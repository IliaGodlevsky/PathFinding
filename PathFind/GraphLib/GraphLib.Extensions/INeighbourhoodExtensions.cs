using Common.Extensions.EnumerableExtensions;
using GraphLib.Exceptions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace GraphLib.Extensions
{
    public static class INeighbourhoodExtensions
    {
        public static IReadOnlyCollection<IVertex> GetNeighboursWithinGraph(this INeighborhood self, IVertex vertex)
        {
            return vertex.Graph.IsNull() 
                ? throw new LonelyVertexException(vertex) 
                : self.GetNeighborsWithinGraphInternal(vertex);
        }

        private static IEnumerable<ICoordinate> WithoutOutOfGraph(this INeighborhood self, IVertex vertex)
        {
            return self.Neighbours.Where(neighbour =>
            {
                bool IsWithin(int coordinate, int graphDimension)
                {
                    var range = new InclusiveValueRange<int>(graphDimension - 1);
                    return range.Contains(coordinate);
                }
                return neighbour.CoordinatesValues.Juxtapose(vertex.Graph.DimensionsSizes, IsWithin);
            });
        }

        private static IReadOnlyCollection<IVertex> GetNeighborsWithinGraphInternal(this INeighborhood self, IVertex vertex)
        {
            return self.WithoutOutOfGraph(vertex)
                .Select(coordinate => vertex.Graph.Get(coordinate))
                .ToArray();
        }
    }
}