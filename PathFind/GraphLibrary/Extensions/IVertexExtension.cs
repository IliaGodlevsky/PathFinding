using GraphLibrary.DTO;
using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphLibrary.Extensions
{
    public static class IVertexExtension
    {
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
            vertex.MarkAsSimpleVertex();
            vertex.ParentVertex = NullVertex.GetInstance();
        }

        public static void Initialize(this IVertex vertex)
        {
            vertex.Neighbours = new List<IVertex>();
            vertex.SetToDefault();
            vertex.IsObstacle = false;
        }

        public static void Initialize(this IVertex vertex, VertexInfo info)
        {
            vertex.IsObstacle = info.IsObstacle;
            vertex.Cost = info.Cost;
            if (vertex.IsObstacle)
                vertex.MarkAsObstacle();
        }

        public static void WashVertex(this IVertex vertex)
        {
            vertex.IsObstacle = true;
        }

        public static bool IsSimpleVertex(this IVertex vertex)
        {
            return !vertex.IsStart && !vertex.IsEnd;
        }

        public static IEnumerable<IVertex> GetUnvisitedNeighbours(this IVertex vertex)
        {
            return vertex.Neighbours.Where(vert => !vert.IsVisited);
        }

        public static IVertex Refresh(this IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
            return vertex;
        }

        public static void SetLocation(this IVertex vertex, Point location)
        {
            vertex.Location = location;
        }
    }
}