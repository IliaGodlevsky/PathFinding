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

        public string GetStatistics(string format)
        {
            return new Statistics(pathLength, steps, 
                new DateTime(watch.ElapsedTicks), visitedVertices).
                GetFormattedData(format);
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
