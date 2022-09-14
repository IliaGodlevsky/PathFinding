using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
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
            return (int)Math.Round(self.Count == 0 ? 0 : self.GetObstaclesCount() * 100.0 / self.Count);
        }

        public static int GetObstaclesCount(this IGraph self)
        {
            return self.Where(vertex => vertex.IsObstacle).Count();
        }

        public static int GetAvailableIntermediatesNumber(this IGraph graph)
        {
            const int NumberOfRequiredEndPoints = 2;
            int isolatedCount = graph.Where(vertex => vertex.IsIsolated()).Count();
            int number = graph.Count - (isolatedCount + NumberOfRequiredEndPoints);
            return number > 0 ? number : 0;
        }
    }
}