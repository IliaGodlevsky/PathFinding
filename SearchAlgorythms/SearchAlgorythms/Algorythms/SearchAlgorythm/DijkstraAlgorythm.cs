using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SearchAlgorythms.Algorythms.Statistics;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class DijkstraAlgorythm : ISearchAlgorythm
    {
        protected readonly IGraphTop end;
        protected List<IGraphTop> tops 
            = new List<IGraphTop>();
        WeightedGraphSearchAlgoStatistics statCollector;

        public DijkstraAlgorythm(IGraphTop end, IGraph graph)
        {
            foreach(var top in graph)
            {
                if (!(top as IGraphTop).IsObstacle)
                {
                    tops.Add(top as IGraphTop);
                    (top as IGraphTop).Value = int.MaxValue;
                }
            }
            statCollector = new WeightedGraphSearchAlgoStatistics();
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
                statCollector.AddLength(int.Parse(top.Text));
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
            tops.Sort((t1, t2) => t1.Value.CompareTo(t2.Value));
            var chippest = GetChippestValue();
            return tops.Find(t => chippest == t.Value && !t.IsVisited);
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

        public bool FindDestionation(IGraphTop start)
        {
            if (end == null)
                return false;
            statCollector.BeginCollectStatistic();
            var currentTop = start;
            start.IsVisited = true;
            start.Value = 0;
            do
            {
                ExtractNeighbours(currentTop);
                currentTop = GetChippestUnvisitedTop();
                if (currentTop.Value == int.MaxValue)
                    break;
                if (IsRightCellToVisit(currentTop))
                    Visit(currentTop);
                Pause(10);
            } while (!IsDestination(currentTop));
            statCollector.StopCollectStatistics();
            return end.IsVisited;
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
                statCollector.CellVisited();
            }
        }

        public string GetStatistics()
        {
            return statCollector.Statistics;
        }
    }
}
