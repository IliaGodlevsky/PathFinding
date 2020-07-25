using GraphLibrary.Vertex;
using System;
using System.Diagnostics;

namespace GraphLibrary.Statistics
{
    public class StatisticsCollector : IStatisticsCollector
    {
        private int visitedVertices;
        private double pathLength;
        private int steps;
        private Stopwatch watch;

        public StatisticsCollector()
        {
            watch = new Stopwatch();
        }

        public string GetFormatedStatistics(string format = "")
        {
            var statistics = GetStatistics();
            if (format == "")
                format = GraphLibraryResources.StatisticsTransformFormat;
            return string.Format(format,
                statistics.Duration.Minute,
                statistics.Duration.Second,
                statistics.Duration.Millisecond,
                statistics.Steps,
                statistics.PathLength,
                statistics.VisitedVertices);
        }

        public Statistics GetStatistics()
        {
            return new Statistics(pathLength, steps, new DateTime(watch.ElapsedTicks), visitedVertices);
        }

        public void IncludeVertexInStatistics(IVertex vertex)
        {
            pathLength += double.Parse(vertex.Text);
            steps++;
        }

        public void StartCollect() => watch.Start();
        public void StopCollect() => watch.Stop();
        public void Visited() => visitedVertices++;
    }
}
