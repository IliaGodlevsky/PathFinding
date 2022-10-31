using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class GraphExtensions
    {
        public static void Refresh<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex, IVisualizable
        {
            graph.ForEach(vertex => vertex.Refresh());
        }

        public static int GetObstaclePercent<TVertex>(this IGraph<TVertex> self)
            where TVertex : IVertex
        {
            return (int)Math.Round(self.Count == 0 ? 0 : self.GetObstaclesCount() * 100.0 / self.Count);
        }

        public static int GetObstaclesCount<TVertex>(this IGraph<TVertex> self)
            where TVertex : IVertex
        {
            return self.Where(vertex => vertex.IsObstacle).Count();
        }

        public static int GetAvailableIntermediatesNumber<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            const int NumberOfRequiredEndPoints = 2;
            int isolatedCount = graph.Where(vertex => vertex.IsIsolated()).Count();
            int number = graph.Count - (isolatedCount + NumberOfRequiredEndPoints);
            return number > 0 ? number : 0;
        }

        public static string GetStringRepresentation<TVertex>(this IGraph<TVertex> graph,
            string format = "Obstacle percent: {0} ({1}/{2})")
            where TVertex : IVertex
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