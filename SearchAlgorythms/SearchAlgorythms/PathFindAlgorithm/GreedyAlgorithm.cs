using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SearchAlgorythms.Extensions.ListExtensions;
using SearchAlgorythms.Statistics;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    /// <summary>
    /// Greedy algorithm. Each step looks for the chippest top and visit it
    /// </summary>
    public class GreedyAlgorithm : IPathFindAlgorithm
    {
        private Stack<IGraphTop> stack = new Stack<IGraphTop>();
        private WeightedGraphSearchAlgoStatistics statCollector;
        private readonly IGraphTop end;

        public GreedyAlgorithm(IGraphTop end)
        {
            statCollector = new WeightedGraphSearchAlgoStatistics();
            this.end = end;
        }

        private IGraphTop GoChippestNeighbour(IGraphTop top)
        {
            List<IGraphTop> neighbours = top.Neighbours.Count(t => t.IsVisited) == 0 
                ? top.Neighbours : top.Neighbours.Where(t => !t.IsVisited).ToList();
            neighbours.Shuffle();
            if (neighbours.Any())
            {
                double min = neighbours.Min(t => int.Parse(t.Text));
                return neighbours.Find(t => t.Text == min.ToString());
            }
            return null;
        }

        public PauseCycle Pause { set; get; }

        public void DrawPath()
        {
            var top = end;
            while (!top.IsStart)
            {
                var temp = top;
                top = top.ParentTop;
                if (top.IsSimpleTop)
                    top.MarkAsPath();
                statCollector.AddLength(int.Parse(temp.Text));
                Pause(35);
            }
        }

        public bool FindDestionation(IGraphTop start)
        {
            if (end == null)
                return false;
            statCollector.BeginCollectStatistic();
            var currentTop = start;
            IGraphTop temp = null;
            Visit(currentTop);
            while(!IsDestination(currentTop))
            {
                temp = currentTop;
                currentTop = GoChippestNeighbour(currentTop);
                if (IsRightCellToVisit(currentTop))
                {
                    Visit(currentTop);
                    currentTop.ParentTop = temp;
                }
                else
                    currentTop = stack.Pop();
                Pause(2);
            }
            statCollector.StopCollectStatistics();
            return end.IsVisited;
        }

        private bool IsDestination(IGraphTop top)
        {
            return top.IsEnd && top.IsVisited || !stack.Any();
        }

        private bool IsRightCellToVisit(IGraphTop top)
        {
            if (top == null)
                return false;
            if (top.IsObstacle)
                return false;
            return true;
        }

        private void Visit(IGraphTop top)
        {
            top.IsVisited = true;
            stack.Push(top);
            if (top.IsSimpleTop)
            {
                top.MarkAsCurrentlyLooked();
                Pause(8);
                top.MarkAsVisited();
            }
            statCollector.CellVisited();
        }

        public string GetStatistics()
        {
            return statCollector.Statistics;
        }
    }
}
