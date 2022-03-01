using GraphLib.Interfaces;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class INeighbourhoodExtensions
    {
        public static IReadOnlyCollection<IVertex> GetNeighbours(this INeighborhood self, IVertex vertex)
        {
            if (vertex.Graph.IsNull())
            {
                string format = "Vertex doesn't belong to any graph. Vertex: {0}";
                string message = string.Format(format, vertex.GetInforamtion());
                throw new ArgumentNullException(message);
            }

            return self
                .Neighbours
                .Where(coordinate => coordinate.IsWithinGraph(vertex.Graph))
                .Select(coordinate => vertex.Graph[coordinate])
                .ToArray();
        }
    }
}
