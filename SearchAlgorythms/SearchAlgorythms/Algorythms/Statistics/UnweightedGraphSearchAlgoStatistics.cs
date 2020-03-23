using System;

namespace SearchAlgorythms.Algorythms.Statistics
{
    public class UnweightedGraphSearchAlgoStatistics : AbstractStatistics
    {
        private int stepsNumber;
        public override string Statistics => 
            base.Statistics + "\nSteps: " + stepsNumber 
            + "\nCells visited: " + visitedCells;

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
