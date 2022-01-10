using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GraphLib.Extensions
{
    public static class IVisitedVerticesExtensions
    {
        /// <summary>
        /// Returns all unvisited neighbours for <paramref name="vertex"/>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IVertex> GetUnvisitedNeighbours(this IVisitedVertices self, IVertex vertex)
        {
            return vertex.Neighbours.Where(self.IsNotVisited);
        }

        /// <summary>
        /// Determines whether the <paramref name="vertex"/> is visited
        /// </summary>
        /// <param name="self"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public static bool HasUnvisitedNeighbours(this IVisitedVertices self, IVertex vertex)
        {
            return vertex.Neighbours.Any(self.IsNotVisited);
        }
    }
}
