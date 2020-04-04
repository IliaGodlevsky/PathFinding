using System.Collections.Generic;
using System.Linq;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Statistics;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    /// <summary>
    /// Finds the chippest path to destination top. 
    /// </summary>
    public class DijkstraAlgorithm : IPathFindAlgorithm
    {
        protected readonly AbstractGraph graph;
        private List<IGraphTop> queue = new List<IGraphTop>();
        private WeightedGraphSearchAlgoStatistics statCollector;

        public DijkstraAlgorithm(AbstractGraph graph)
        {
            this.graph = graph;
            foreach (var top in graph)
                (top as IGraphTop).Value = double.PositiveInfinity;
            statCollector = new WeightedGraphSearchAlgoStatistics();
        }

        public PauseCycle Pause { set; get; }

        public void DrawPath()
        {
            var top = graph.End;
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

        private IGraphTop GetChippestUnvisitedTop()
        {
            queue.RemoveAll(t => t.IsVisited);
            queue.Sort((t1, t2) => t1.Value.CompareTo(t2.Value));
            return queue.Any() ? queue.First() : null;
        }

        public virtual double GetPathValue(IGraphTop neighbour, IGraphTop top)
        {
            return int.Parse(neighbour.Text) + top.Value;
        }

        private void ExtractNeighbours(IGraphTop button)
        {
            if (button is null)
                return;
            var neighbours = button.Neighbours;
            foreach(var neighbour in neighbours)
            {
                if (!neighbour.IsVisited)
                    queue.Add(neighbour);
                if (neighbour.Value > GetPathValue(neighbour, button))
                {                    
                    neighbour.Value = GetPathValue(neighbour, button);
                    neighbour.ParentTop = button;
                }
            }
        }

        public bool FindDestionation()
        {
            if (graph.End == null)
                return false;
            statCollector.BeginCollectStatistic();
            var currentTop = graph.Start;
            currentTop.IsVisited = true;
            currentTop.Value = 0;
            do
            {
                ExtractNeighbours(currentTop);
                currentTop = GetChippestUnvisitedTop();
                if (currentTop?.Value == double.PositiveInfinity
                    || currentTop == null)
                    break;
                if (IsRightCellToVisit(currentTop))
                    Visit(currentTop);
                Pause(2);
            } while (!IsDestination(currentTop));
            statCollector.StopCollectStatistics();
            return graph.End.IsVisited;
        }

        private bool IsDestination(IGraphTop button)
        {
            if (button == null)
                return false;
            if (button.IsObstacle)
                return false;
            return button.IsEnd && button.IsVisited;
        }

        private bool IsRightCellToVisit(IGraphTop button)
        {
            if (button is null)
                return false;
            if (button.IsObstacle)
                return false;
            else
                return !button.IsVisited;
        }

        private void Visit(IGraphTop button)
        {
            button.IsVisited = true;
            if (!button.IsEnd)
            {
                button.MarkAsCurrentlyLooked();
                Pause(8);
                button.MarkAsVisited();               
            }
            statCollector.CellVisited();
        }

        public string GetStatistics()
        {
            return statCollector.Statistics;
        }
    }
}
