using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
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

        public static string GetStringRepresentation(this IGraph graph,
            string format = "Obstacle percent: {0} ({1}/{2})")
        {
            if (!graph.IsNull())
            {
                const string LargeSpace = "   ";
                var dimensionNames = new[] { "Width", "Length", "Height" };
                int obstacles = graph.GetObstaclesCount();
                int obstaclesPercent = graph.GetObstaclePercent();
                string Zip(string name, int size) => $"{name}: {size}";
                var zipped = dimensionNames.Zip(graph.DimensionsSizes, Zip);
                string joined = string.Join(LargeSpace, zipped);
                string graphParams = string.Format(format,
                    obstaclesPercent, obstacles, graph.Count);
                return string.Join(LargeSpace, joined, graphParams);
            }
            return string.Empty;
        }
    }
}