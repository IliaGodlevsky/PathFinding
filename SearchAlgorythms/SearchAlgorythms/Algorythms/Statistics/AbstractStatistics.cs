using System.Diagnostics;

namespace SearchAlgorythms.Algorythms.Statistics
{
    public abstract class AbstractStatistics
    {
        protected Stopwatch watch = new Stopwatch();
        protected int visitedCells;

        public virtual string Statistics { get { return Time; } }

        public virtual void BeginCollectStatistic()
        {
            visitedCells = 0;
            watch.Start();
        }

        private string Time => "Time: " + (watch.Elapsed.Minutes + ":" + watch.Elapsed.Seconds + "." +
                +watch.Elapsed.Milliseconds);

        public void StopCollectStatistics()
        {
            watch.Stop();
        }

        public void CellVisited() => visitedCells++;
    }
}
