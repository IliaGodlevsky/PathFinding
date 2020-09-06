using GraphLibrary.Collection;
using GraphLibrary.Extensions;
using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Vertex;
using System.Diagnostics;
using System.Linq;

namespace GraphLibrary.Common.Extensions
{
    public static class GraphExtensions
    {
        private static IVertex RefreshVertex(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
            return vertex;
        }

        public static string GetFormattedInfo(this Graph graph, string format)
        {
            return string.Format(format, graph.Width, graph.Height,
               graph.ObstaclePercent, graph.ObstacleNumber, graph.Size);
        }

        public static string GetFormattedStatistics(this Graph graph, Stopwatch timer, string format)
        {
            var pathLength = graph.End.GetPathToStartVertex().Sum(vertex =>
            {
                if (vertex.IsStart)
                    return 0;
                return vertex.Cost;
            });
            var steps = graph.End.GetPathToStartVertex().Count() - 1;
            var visitedVertices = graph.NumberOfVisitedVertices;

            return string.Format(format, timer.Elapsed.Minutes,
                    timer.Elapsed.Seconds, timer.Elapsed.Milliseconds,
                    steps, pathLength, visitedVertices);
        }

        public static void Refresh(this Graph graph)
        {
            graph.RemoveExtremeVertices();
            graph.GetArray().Apply(RefreshVertex);
        }

        public static void RemoveExtremeVertices(this Graph graph)
        {
            graph.End = null;
            graph.Start = null;
        }
    }
}
