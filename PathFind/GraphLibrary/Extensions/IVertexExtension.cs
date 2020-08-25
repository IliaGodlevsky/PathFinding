using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Extensions
{
    public static class IVertexExtension
    {
        public static bool IsValidToBeRange(this IVertex vertex)
        {
            return vertex.IsSimpleVertex && !vertex.IsIsolated();
        }

        public static bool IsIsolated(this IVertex vertex)
        {
            return vertex.IsObstacle || !vertex.Neighbours.Any();
        }
    }
}
