using System;

namespace SearchAlgorythms.Statistics
{
    public class WeightedGraphSearchAlgoStatistics : AbstractStatistics
    {
        private int pathLength;
        public override string Statistics => base.Statistics + "\nPath length: " + pathLength;

        public override void BeginCollectStatistic()
        {
            base.BeginCollectStatistic();
            pathLength = 0;
        }

        public void AddLength(int stepLength) => pathLength += stepLength;
    }
}
