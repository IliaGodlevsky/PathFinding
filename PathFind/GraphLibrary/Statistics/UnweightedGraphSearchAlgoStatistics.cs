using System;

namespace GraphLibrary.Statistics
{
    public class UnweightedGraphSearchAlgoStatisticsCollector : AbstractStatisticsCollector
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
