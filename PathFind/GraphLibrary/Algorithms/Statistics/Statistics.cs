using System;

namespace GraphLibrary.Statistics
{
    public class Statistics
    {
        private const string format = "Time: {0}:{1}.{2}  Steps: {3}   Path length: {4}   Visited vertices: {5}";

        public Statistics(double pathLength, int steps, DateTime duration, int visitedVertices)
        {
            PathLength = pathLength;
            Steps = steps;
            Duration = duration;
            VisitedVertices = visitedVertices;
        }

        public double PathLength { get; private set; }
        public DateTime Duration { get; private set; }
        public int Steps { get; private set; }
        public int VisitedVertices { get; private set; }

        public string GetFormattedData(string format = format)
        {
            return string.Format(format,
                Duration.Minute,
                Duration.Second,
                Duration.Millisecond,
                Steps,
                PathLength,
                VisitedVertices);
        }
    }
}
