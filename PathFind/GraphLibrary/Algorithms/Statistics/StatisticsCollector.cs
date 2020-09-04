using GraphLibrary.Collection;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Extensions;
using System.Diagnostics;
using System.Linq;

namespace GraphLibrary.Statistics
{
    public class StatisticsCollector : IStatisticsCollector
    {        
        private int visitedVertices;
        private double pathLength;
        private int steps;
        private Stopwatch timer;

        public StatisticsCollector()
        {

        }

        public void CollectStatistics(Graph graph, Stopwatch timer)
        {
            this.timer = timer;
            pathLength = graph.End.GetPathToStartVertex().Sum(vertex =>
            {
                if (vertex.IsStart)
                    return 0;
                return vertex.Cost;
            });
            steps = graph.End.GetPathToStartVertex().Count() - 1;
            visitedVertices = graph.GetNumberOfVisitedVertices();
        }

        public string GetStatistics(string format)
        {
            return string.Format(format, timer.Elapsed.Minutes,
                    timer.Elapsed.Seconds, timer.Elapsed.Milliseconds,
                    steps, pathLength, visitedVertices);           
        }
    }
}
