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

        public static int GetObstaclePercent(this IGraph self)
        {
            return (int)Math.Round(self.Size == 0 ? 0 : self.GetObstaclesCount() * 100.0 / self.Size);
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
            int isolatedCount = graph.Vertices.Where(vertex => vertex.IsIsolated()).Count();
            int number = graph.Size - isolatedCount - NumberOfRequiredEndPoints;
            return number > 0 ? number : 0;
        }
    }
}