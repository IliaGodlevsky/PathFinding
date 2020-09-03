using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;
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
            pathLength += vertex.Cost;
            steps++;
        }

        public void IncludeVerticesInStatistics(IEnumerable<IVertex> collection)
        {
            foreach (var vertex in collection)
                IncludeVertexInStatistics(vertex);
        }

        public void StartCollect() => watch.Start();
        public void StopCollect() => watch.Stop();
        public void Visited() => visitedVertices++;
    }
}
