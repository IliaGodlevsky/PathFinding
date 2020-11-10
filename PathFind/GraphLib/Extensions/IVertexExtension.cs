using GraphLib.Info;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class IVertexExtension
    {
        public static IEnumerable<IVertex> GetPathToStartVertex(this IVertex vertex)
        {
            if (vertex.IsEnd && vertex.IsVisited && !vertex.IsDefault)
            {
                var temp = vertex;

                while (!temp.IsStart && !temp.IsDefault)
                {
                    yield return temp;
                    temp = temp.ParentVertex;
                }
            }
        }
        public static bool IsValidToBeRange(this IVertex vertex)
        {
            return vertex.IsSimpleVertex() && !vertex.IsIsolated();
        }

        public static bool IsIsolated(this IVertex vertex)
        {
            return vertex.IsObstacle || !vertex.Neighbours.Any();
        }

        public static void SetToDefault(this IVertex vertex)
        {
            vertex.IsStart = false;
            vertex.IsEnd = false;
            vertex.IsVisited = false;
            vertex.AccumulatedCost = 0;
            vertex.ParentVertex = new DefaultVertex();
            vertex.MarkAsSimpleVertex();
        }

        public static void Initialize(this IVertex vertex)
        {
            vertex.Neighbours = new List<IVertex>();
            vertex.SetToDefault();
            vertex.IsObstacle = false;
        }

        public static void Initialize(this IVertex vertex, VertexInfo info)
        {
            vertex.Position = info.Position;
            vertex.Cost = info.Cost;
            vertex.IsObstacle = info.IsObstacle;
            if (vertex.IsObstacle)
            {
                vertex.MarkAsObstacle();
            }
        }

        public static void WashVertex(this IVertex vertex)
        {
            vertex.IsObstacle = true;
        }

        public static bool IsSimpleVertex(this IVertex vertex)
        {
            return !vertex.IsStart && !vertex.IsEnd;
        }

        public static IEnumerable<IVertex> GetUnvisitedNeighbours(this IVertex self)
        {
            return self.Neighbours.Where(vertex => !vertex.IsVisited);
        }

        public static IVertex Refresh(this IVertex vertex)
        {
            if (!vertex.IsObstacle)
            {
                vertex.SetToDefault();
            }

            return vertex;
        }
    }
}