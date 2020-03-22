using System.Collections.Generic;
using System.Diagnostics;
using SearchAlgorythms.Extensions;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class GreedySearch : ISearchAlgorythm
    {
        private Stack<IGraphTop> stack =
            new Stack<IGraphTop>();

        private readonly IGraphTop end;

        private Stopwatch watch = new Stopwatch();

        public GreedySearch(IGraphTop end)
        {
            this.end = end;
        }

        private IGraphTop GoChippestNeighbour(IGraphTop top)
        {
            int min = 0;
            var neighbours = top.Neighbours;
            foreach(var neighbour in neighbours)
            {
                if (!neighbour.IsVisited)
                    min = int.Parse(neighbour.Text);
            }
            foreach(var neighbour in neighbours)
            {
                if (min > int.Parse(neighbour.Text)
                    && (!neighbour.IsVisited || neighbour.IsEnd)) 
                    min = int.Parse(neighbour.Text);
            }
            return neighbours.Find(t => min ==
                int.Parse(t.Text) && !t.IsVisited);
        }

        public bool DestinationFound { get; set; }
        public PauseCycle Pause { set; get; }

        public void DrawPath(IGraphTop end)
        {
            var top = end;
            while (!top.IsStart)
            {
                top = top.ParentTop;
                if (top.IsSimpleTop)
                    top.MarkAsPath();
                Pause(250);
            }
        }

        public void ExtractNeighbours(IGraphTop button)
        {
            return;
        }

        public void FindDestionation(IGraphTop start)
        {
            watch.Start();
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
                Pause(25);
            }
            watch.Stop();
            DestinationFound = end.IsVisited;
        }

        public int GetTime()
        {
            return watch.Elapsed.Seconds + watch.Elapsed.Minutes * 60;
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
                top.MarkAsVisited();
        }

        public bool CanStartSearch()
        {
            return end != null;
        }
    }
}
