namespace GraphLibrary.Statistics
{
    public class WeightedGraphSearchAlgoStatisticsCollector : AbstractStatisticsCollector
    {
        protected int pathLength;
        public override string Statistics => base.Statistics + "\nPath length: " + pathLength;

        public override void BeginCollectStatistic()
        {
            base.BeginCollectStatistic();
            pathLength = 0;
        }

        public void AddLength(int stepLength) => pathLength += stepLength;
    }
}
