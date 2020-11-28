using GraphLib.Graphs.Abstractions;
using GraphLib.Info;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class IVertexExtension
    {
        public static bool IsValidToBeExtreme(this IVertex vertex)
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

        internal static void Refresh(this IVertex vertex)
        {
            if (!vertex.IsObstacle)
            {
                vertex.SetToDefault();
            }
        }

        internal static void Isolate(this IVertex self)
        {
            foreach (var neigbour in self.Neighbours)
            {
                neigbour.Neighbours.Remove(self);
            }

            self.Neighbours.Clear();
        }

        internal static VertexInfo GetInfo(this IVertex self)
        {
            return new VertexInfo(self);
        }

        public static void ConnectWithNeighbours(this IVertex self)
        {
            foreach (var neigbour in self.Neighbours)
            {
                if (self.CanBeNeighbourOf(neigbour))
                {
                    neigbour.Neighbours.Add(self);
                }
            }
        }

        internal static void SetNeighbours(this IVertex self, IGraph graph)
        {
            if (!self.IsObstacle)
            {
                var vertexEnvironment = self.GetEnvironment(graph);
                foreach (var neighbourCandidate in vertexEnvironment)
                {
                    if (neighbourCandidate.CanBeNeighbourOf(self))
                    {
                        self.Neighbours.Add(neighbourCandidate);
                    }
                }
            }
        }

        internal static IEnumerable<IVertex> GetEnvironment(this IVertex self, IGraph graph)
        {
            foreach (var coordinate in self.Position.Environment)
            {
                if (coordinate.IsWithinGraph(graph))
                {
                    yield return graph[coordinate];
                }
            }
        }

        private static bool CanBeNeighbourOf(this IVertex self, IVertex vertex)
        {
            return !self.IsObstacle && !ReferenceEquals(vertex, self)
                && !vertex.Neighbours.Contains(self);
        }
    }
}