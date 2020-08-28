using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

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

        public static void SetToDefualt(this IVertex vertex)
        {
            vertex.IsStart = false;
            vertex.IsEnd = false;
            vertex.IsVisited = false;
            vertex.Value = 0;
            vertex.MarkAsSimpleVertex();
            vertex.ParentVertex = null;
        }

        public static void Initialize(this IVertex vertex)
        {
            vertex.Neighbours = new List<IVertex>();
            vertex.SetToDefualt();
            vertex.IsObstacle = false;
        }

        public static void Initialize(this IVertex vertex, VertexInfo info)
        {
            vertex.IsObstacle = info.IsObstacle;
            vertex.Text = info.Text;
            if (vertex.IsObstacle)
                vertex.MarkAsObstacle();
        }
    }
}
