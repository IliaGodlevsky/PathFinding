using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class IVisitedVerticesExtensions
    {
        public static IEnumerable<IVertex> GetUnvisitedNeighbours(this IVisitedVertices self, IVertex vertex)
        {
            return vertex.Neighbours.Where(self.IsNotVisited);
        }

        public static bool HasUnvisitedNeighbours(this IVisitedVertices self, IVertex vertex)
        {
            return vertex.Neighbours.Any(self.IsNotVisited);
        }
    }
}
