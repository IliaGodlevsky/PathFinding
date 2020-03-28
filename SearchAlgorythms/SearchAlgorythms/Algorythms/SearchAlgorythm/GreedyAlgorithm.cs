using System;
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
            bool predicate(IGraphTop t) => !t.IsVisited || t.IsEnd;
            double func(IGraphTop t) => int.Parse(t.Text);
            int min = (int)DelegatedMethod.GetMinValue(top.Neighbours, 
               predicate, func);
            return top.Neighbours.Find(t => min == func(t) && predicate(t));
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
                Pause(20);
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
            return top.IsEnd && top.IsVisited || stack.IsEmpty();
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
