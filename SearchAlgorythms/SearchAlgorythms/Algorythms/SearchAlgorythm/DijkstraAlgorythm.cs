using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SearchAlgorythms.ButtonExtension;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class DijkstraAlgorythm : ISearchAlgorythm
    {
        private readonly GraphTop end;
        private List<GraphTop> tops 
            = new List<GraphTop>();
        private Stopwatch watch = new Stopwatch();

        protected void Pause(int value = 0)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        public DijkstraAlgorythm(GraphTop end, IGraph graph)
        {
            for (int i = 0; i < graph.GetWidth(); i++)
            {
                for (int j = 0; j < graph.GetHeight(); j++)
                {
                    if (!graph[i, j].IsObstacle())
                    {               
                        tops.Add(graph[i, j] as GraphTop);
                        (graph[i, j] as GraphTop).Value = int.MaxValue;
                    }                      
                }
            }
            this.end = end;
        }

        public bool DestinationFound { get; set ; }

        public void DrawPath(GraphTop end)
        {
            var top = end;
            while (!top.IsStart)
            {
                top = top.ParentTop;
                if (!top.IsEnd && !top.IsStart)
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

        private GraphTop GetChippestUnvisitedTop()
        {
            return tops.Find(t => GetChippestValue() == t.Value
                    && !t.IsVisited);
        }

        public void ExtractNeighbours(Button button)
        {
            var top = button as GraphTop;
            var neighbours = top.GetNeighbours();
            foreach(var neighbour in neighbours)
            {
                if (neighbour.Value > int.Parse(neighbour.Text) + top.Value) 
                {                  
                    neighbour.Value = int.Parse(neighbour.Text) + top.Value;
                    neighbour.ParentTop = top;
                }
            }
        }

        private bool IsNoTopToMark() => GetChippestValue() == int.MaxValue;

        public void FindDestionation(GraphTop start)
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
                Pause(10);
            } while (!IsDestination(currentTop));
            watch.Stop();
            DestinationFound = end.IsVisited 
                && !IsNoTopToMark();
        }

        public int GetTime()
        {
            return watch.Elapsed.Seconds + watch.Elapsed.Minutes * 60;
        }

        public bool IsDestination(Button button)
        {
            var top = button as GraphTop;
            if (top == null)
                return false;
            if (top.IsObstacle())
                return false;
            return top.IsEnd && top.IsVisited;
        }

        public bool IsRightCellToVisit(Button button)
        {
            GraphTop top = button as GraphTop;
            if (top.IsObstacle())
                return false;
            else
                return !top.IsVisited;
        }

        public void Visit(Button button)
        {
            var top = button as GraphTop;
            top.IsVisited = true;
            if (!top.IsEnd)
                top.MarkAsVisited();
        }
    }
}
