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
    }
}
