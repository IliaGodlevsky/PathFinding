using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class VisitedVerticesExtensions
    {
        public static bool HasUnvisitedNeighbours(this IVisitedVertices self, IVertex vertex)
        {
            return vertex.Neighbours.Any(self.IsNotVisited);
        }
    }
}
