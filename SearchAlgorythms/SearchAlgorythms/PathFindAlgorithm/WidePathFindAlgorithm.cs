using System.Collections.Generic;
using System.Linq;
using SearchAlgorythms.Top;
using SearchAlgorythms.Extensions.ListExtensions;
using SearchAlgorythms.Statistics;

namespace SearchAlgorythms.Algorithm
{
    public class WidePathFindAlgorithm : IPathFindAlgorithm
    {
        protected Queue<IGraphTop> queue = new Queue<IGraphTop>();
        private UnweightedGraphSearchAlgoStatistics statCollector;
        protected IGraphTop end;


        public WidePathFindAlgorithm(IGraphTop end)
        {
            this.end = end;
            statCollector = new UnweightedGraphSearchAlgoStatistics();
        }

        public IGraphTop GoChippestNeighbour(IGraphTop top)
        {
            top.Neighbours.Shuffle();
            double min = top.Neighbours.Min(t => t.Value);
            return top.Neighbours.Find(t => min == t.Value
                    && t.IsVisited && IsRightNeighbour(t));
        }

        public virtual bool IsRightNeighbour(IGraphTop top)
        {
            return !top.IsEnd;
        }

        public virtual bool IsRightPath(IGraphTop top)
        {
            return !top.IsStart;
        }

        public virtual bool IsRightCellToVisit(IGraphTop button)
        {
            return !button.IsVisited;
        }

        public PauseCycle Pause { get; set; }

        public virtual void ExtractNeighbours(IGraphTop button)
        {
            if (button is null)
                return;
            foreach (var neigbour in button.Neighbours)
            {
                if (neigbour.Value == 0 && !neigbour.IsStart)
                    neigbour.Value = button.Value + 1;
                if (!neigbour.IsVisited)
                    queue.Enqueue(neigbour);
            }
        }

        public virtual bool FindDestionation(IGraphTop start)
        {
            if (end == null)
                return false;
            statCollector.BeginCollectStatistic();
            var currentTop = start;
            Visit(currentTop);
            while (!IsDestination(currentTop))
            {
                currentTop = queue.Dequeue();
                if (IsRightCellToVisit(currentTop))
                    Visit(currentTop);
                Pause(2);              
            }
            statCollector.StopCollectStatistics();
            return end.IsVisited;          
        }

        public void DrawPath()
        {
            var top = end;
            while (IsRightPath(top))
            {
                top = GoChippestNeighbour(top);
                if (top.IsSimpleTop)
                    top.MarkAsPath();
                statCollector.AddStep();
                Pause(35);
            }
        }

        private bool IsDestination(IGraphTop button)
        {
            if (button is null)
                return false;
            return button.IsEnd || !queue.Any();
        }

        private void Visit(IGraphTop top)
        {          
            if (top.IsObstacle)
                return;
            top.IsVisited = true;
            if (top.IsSimpleTop)
            {
                top.MarkAsCurrentlyLooked();
                Pause(8);
                top.MarkAsVisited();
            }
            statCollector.CellVisited();
            ExtractNeighbours(top);
        }

        public string GetStatistics()
        {
            return statCollector.Statistics;
        }
    }
}
