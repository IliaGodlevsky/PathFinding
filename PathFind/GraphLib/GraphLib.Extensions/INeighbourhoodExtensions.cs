using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class INeighbourhoodExtensions
    {
        /// <summary>
        /// Sets certain vertices of <paramref name="self"/>'s 
        /// environment as its neighbors
        /// </summary>
        /// <param name="self"></param>
        /// <param name="graph">A graph, where vertex is situated</param>
        /// <exception cref="ArgumentException"></exception>
        public static IReadOnlyCollection<IVertex> GetNeighbours(this INeighborhood self, IVertex vertex)
        {
            if (vertex.Graph == null)
            {
                string message = $"Vertex doesn't belong to any graph. Vertex: {vertex.GetInforamtion()}";
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
