using System.Diagnostics;

namespace GraphLibrary.Statistics
{
    public abstract class AbstractStatisticsCollector
    {
        private Stopwatch watch = new Stopwatch();
        protected int visitedCells;

        public virtual string Statistics
        {
            get
            {
                return Time + "\nCells visited: "
                + visitedCells;
            }
        }

        public virtual void BeginCollectStatistic()
        {
            visitedCells = 0;
            watch.Start();
        }

        protected string Time => "Time: " + (watch.Elapsed.Minutes 
            + ":" + watch.Elapsed.Seconds + "." +
                +watch.Elapsed.Milliseconds);

        public void StopCollectStatistics()
        {
            watch.Stop();
        }

        public void CellVisited() => visitedCells++;
    }
}
