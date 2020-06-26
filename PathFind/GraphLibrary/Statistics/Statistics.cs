using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
