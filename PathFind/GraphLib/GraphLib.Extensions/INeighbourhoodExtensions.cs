using GraphLib.Exceptions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class INeighbourhoodExtensions
    {
        public static IReadOnlyCollection<IVertex> GetNeighboursWithinGraph(this INeighborhood self, IVertex vertex)
        {
            return vertex.Graph.IsNull() ? throw new LonelyVertexException(vertex) : self.GetNeighborsWithinGraphInternal(vertex);
        }

        private static IEnumerable<ICoordinate> WithoutOutOfGraph(this INeighborhood self, IVertex vertex)
        {
            return self.Neighbours.Where(coordinate => coordinate.IsWithinGraph(vertex.Graph));
        }

        private static IReadOnlyCollection<IVertex> GetNeighborsWithinGraphInternal(this INeighborhood self, IVertex vertex)
        {
            return self.WithoutOutOfGraph(vertex).Select(coordinate => vertex.Graph.GetByCoordinate(coordinate)).ToArray();
        }
    }
}