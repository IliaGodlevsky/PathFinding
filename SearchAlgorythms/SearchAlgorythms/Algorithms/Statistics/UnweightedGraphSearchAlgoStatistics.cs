using System;

namespace SearchAlgorithms.Algorithms.Statistics
{
    public class UnweightedGraphSearchAlgoStatistics : AbstractStatistics
    {
        private int stepsNumber;
        public override string Statistics =>
            base.Statistics + "\nSteps: " + stepsNumber;

        public override void BeginCollectStatistic()
        {
            base.BeginCollectStatistic();
            stepsNumber = 0;
        }

        public void AddStep()
        {
            stepsNumber++;
        }
    }
}
