using System.Collections.Generic;
using SearchAlgorythms.Algorythms.Statistics;
using SearchAlgorythms.DelegatedMethods;
using SearchAlgorythms.Extensions;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class GreedyAlgorithm : ISearchAlgorithm
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
            int min = (int)DelegatedMethod.GetMinValue(top.Neighbours, 
                t => !t.IsVisited || t.IsEnd, t => int.Parse(t.Text));
            return top.Neighbours.Find(t => min == int.Parse(t.Text) && !t.IsVisited || t.IsEnd);
        }

        public PauseCycle Pause { set; get; }

        public void DrawPath()
        {
            var top = end;
            while (!top.IsStart)
            {
                top = top.ParentTop;
                if (top.IsSimpleTop)
                    top.MarkAsPath();
                statCollector.AddLength(int.Parse(top.Text));
                //Pause(500);
            }
        }

        public void ExtractNeighbours(IGraphTop button)
        {
            return;
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

        public bool IsDestination(IGraphTop top)
        {
            return top.IsEnd && top.IsVisited || stack.IsEmpty();
        }

        public bool IsRightCellToVisit(IGraphTop top)
        {
            if (top == null)
                return false;
            if (top.IsObstacle)
                return false;
            return true;
        }

        public void Visit(IGraphTop top)
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
