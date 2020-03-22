using System.Collections.Generic;
using System.Diagnostics;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class DijkstraAlgorythm : ISearchAlgorythm
    {
        protected readonly IGraphTop end;
        protected List<IGraphTop> tops 
            = new List<IGraphTop>();
        protected Stopwatch watch = new Stopwatch();
        private int visitedCells;
        private int pathLength;

        public DijkstraAlgorythm(IGraphTop end, IGraph graph)
        {
            for (int i = 0; i < graph.GetWidth(); i++)
            {
                for (int j = 0; j < graph.GetHeight(); j++)
                {
                    if (!graph[i, j].IsObstacle)
                    {               
                        tops.Add(graph[i, j]);
                        graph[i, j].Value = int.MaxValue;
                    }                      
                }
            }
            this.end = end;
            visitedCells = 0;
            pathLength = 0;
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
                pathLength += int.Parse(top.Text);
                //Pause(250);
            }
        }

        public double GetChippestValue()
        {
            double min = 0;
            foreach (var top in tops)
            {
                if (!top.IsVisited)
                    min = top.Value;
            }
            foreach (var top in tops)
                if (min > top.Value && !top.IsVisited)
                    min = top.Value;
            return min;
        }

        public IGraphTop GetChippestUnvisitedTop()
        {
            return tops.Find(t => GetChippestValue() == t.Value
                    && !t.IsVisited);
        }

        public virtual double GetPathValue(IGraphTop neighbour, IGraphTop top)
        {
            return int.Parse(neighbour.Text) + top.Value;
        }

        public void ExtractNeighbours(IGraphTop button)
        {
            if (button is null)
                return;
            var neighbours = button.Neighbours;
            foreach(var neighbour in neighbours)
            {
                if (neighbour.Value > GetPathValue(neighbour, button))
                {                  
                    neighbour.Value = GetPathValue(neighbour, button);
                    neighbour.ParentTop = button;
                }
            }
        }

        protected bool IsNoTopToMark() => GetChippestValue() == int.MaxValue;

        public bool FindDestionation(IGraphTop start)
        {
            if (end == null)
                return false;
            watch.Start();
            var currentTop = start;
            start.IsVisited = true;
            start.Value = 0;
            do
            {
                ExtractNeighbours(currentTop);
                currentTop = GetChippestUnvisitedTop();
                if (IsNoTopToMark())
                    break;
                if (IsRightCellToVisit(currentTop))
                    Visit(currentTop);
                Pause(10);
            } while (!IsDestination(currentTop));
            watch.Stop();
            return end.IsVisited;
        }

        public int Time => watch.Elapsed.Seconds + watch.Elapsed.Minutes * 60;

        public bool IsDestination(IGraphTop button)
        {
            if (button == null)
                return false;
            if (button.IsObstacle)
                return false;
            return button.IsEnd && button.IsVisited;
        }

        public bool IsRightCellToVisit(IGraphTop button)
        {
            if (button is null)
                return false;
            if (button.IsObstacle)
                return false;
            else
                return !button.IsVisited;
        }

        public void Visit(IGraphTop button)
        {
            button.IsVisited = true;
            if (!button.IsEnd)
            {
                button.MarkAsVisited();
                visitedCells++;
            }
        }

        public string GetStatistics()
        {
            return "Path length: " + pathLength.ToString() + "\n" +
                "Cells visited: " + visitedCells.ToString();
        }
    }
}
