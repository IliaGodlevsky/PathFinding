using System;

namespace GraphLibrary.Statistics
{
    public class Statistics
    {
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

        public override string ToString()
        {
            return string.Format(GraphLibraryResources.StatisticsTransformFormat,
                Duration.Minute,
                Duration.Second,
                Duration.Millisecond,
                Steps,
                PathLength,
                VisitedVertices);
        }
    }
}
