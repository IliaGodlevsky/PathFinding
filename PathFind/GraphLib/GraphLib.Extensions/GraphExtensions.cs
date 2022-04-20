using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class GraphExtensions
    {
        public static void Refresh(this IGraph graph)
        {
            graph.ForEach(vertex => vertex.Refresh());
        }

        public static bool IsEmpty(this IGraph self)
        {
            return !self.Vertices.Any();
        }

        public static bool HasVertices(this IGraph self)
        {
            return !self.IsEmpty();
        }

        public static int GetObstaclePercent(this IGraph self)
        {
            return self.Size == 0 ? 0 : self.GetObstaclesCount() * 100 / self.Size;
        }

        public static int GetObstaclesCount(this IGraph self)
        {
            return self.Vertices.Where(vertex => vertex.IsObstacle).Count();
        }

        public static IGraph ForEach(this IGraph self, Action<IVertex> action)
        {
            self.Vertices.ForEach(action);
            return self;
        }

        public static IGraph ForEach<TVertex>(this IGraph self, Action<TVertex> action)
            where TVertex : IVertex
        {
            foreach (TVertex vertex in self.Vertices)
            {
                action(vertex);
            }
            return self;
        }

        public static int GetAvailableIntermediatesNumber(this IGraph graph)
        {
            const int NumberOfRequiredEndPoints = 2;
            int number = graph.Size - graph.GetIsolatedCount() - NumberOfRequiredEndPoints;
            return number > 0 ? number : 0;
        }

        public static bool HasAvailableEndPoints(this IGraph graph)
        {
            return graph.GetAvailableIntermediatesNumber() > 0;
        }

        public static bool IsEqual(this IGraph self, IGraph graph)
        {
            bool hasEqualDimensionSizes = self.DimensionsSizes.SequenceEqual(graph.DimensionsSizes);
            bool hasEqualNumberOfObstacles = graph.GetObstaclesCount() == self.GetObstaclesCount();
            bool hasEqualVertices = self.Vertices.Juxtapose(graph.Vertices, (a, b) => a.Equals(b));
            return hasEqualNumberOfObstacles && hasEqualVertices && hasEqualDimensionSizes;
        }

        public static IVertex FirstOrNullVertex(this IGraph graph)
        {
            return graph.Vertices.FirstOrDefault() ?? NullVertex.Instance;
        }

        public static IVertex LastOrNullVertex(this IGraph graph)
        {
            return graph.Vertices.LastOrDefault() ?? NullVertex.Instance;
        }

        public static int GetIsolatedCount(this IGraph self)
        {
            return self.Vertices.Where(vertex => vertex.IsIsolated()).Count();
        }
    }
}