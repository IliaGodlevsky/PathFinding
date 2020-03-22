using System.Collections.Generic;
using System.Diagnostics;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class DijkstraAlgorythm : ISearchAlgorythm
    {
        private readonly IGraphTop end;
        private List<IGraphTop> tops 
            = new List<IGraphTop>();
        private Stopwatch watch = new Stopwatch();

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

        private int GetChippestValue()
        {
            int min = 0;
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

        private IGraphTop GetChippestUnvisitedTop()
        {
            return tops.Find(t => GetChippestValue() == t.Value
                    && !t.IsVisited);
        }

        public void ExtractNeighbours(IGraphTop button)
        {
            var neighbours = button.Neighbours;
            foreach(var neighbour in neighbours)
            {
                if (neighbour.Value > int.Parse(neighbour.Text) + button.Value) 
                {                  
                    neighbour.Value = int.Parse(neighbour.Text) + button.Value;
                    neighbour.ParentTop = button;
                }
            }
        }

        private bool IsNoTopToMark() => GetChippestValue() == int.MaxValue;

        public void FindDestionation(IGraphTop start)
        {
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
                Pause(25);
            } while (!IsDestination(currentTop));
            watch.Stop();
            DestinationFound = end.IsVisited 
                && !IsNoTopToMark();
        }

        public int GetTime()
        {
            return watch.Elapsed.Seconds + watch.Elapsed.Minutes * 60;
        }

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
            if (button.IsObstacle)
                return false;
            else
                return !button.IsVisited;
        }

        public void Visit(IGraphTop button)
        {
            button.IsVisited = true;
            if (!button.IsEnd)
                button.MarkAsVisited();
        }

        public bool CanStartSearch()
        {
            return end != null;
        }
    }
}
