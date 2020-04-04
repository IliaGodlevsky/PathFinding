using System.Diagnostics;

namespace SearchAlgorythms.Statistics
{
    public abstract class AbstractStatistics
    {
        private Stopwatch watch = new Stopwatch();
        private int visitedCells;

        public virtual string Statistics
        {
            get
            {
                return Time + "\nCell visited: "
                + visitedCells;
            }
        }

        public virtual void BeginCollectStatistic()
        {
            visitedCells = 0;
            watch.Start();
        }

        private string Time => "Time: " + (watch.Elapsed.Minutes 
            + ":" + watch.Elapsed.Seconds + "." +
                +watch.Elapsed.Milliseconds);

        public void StopCollectStatistics()
        {
            watch.Stop();
        }

        public void CellVisited() => visitedCells++;
    }
}
