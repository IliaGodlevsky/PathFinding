using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SearchAlgorythms.Top;
using SearchAlgorythms.Extensions.ListExtensions;
using SearchAlgorythms.Extensions;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class WideSearch : ISearchAlgorythm
    {
        protected Queue<IGraphTop> queue 
            = new Queue<IGraphTop>();
        private Stopwatch watch = new Stopwatch();
        private readonly IGraphTop end;
        private int cellsVisited;
        private int steps;

        public WideSearch(IGraphTop end)
        {
            this.end = end;
            cellsVisited = 0;
            steps = 0;
        }

        public IGraphTop GoChippestNeighbour(IGraphTop top)
        {
            var chippest = top;
            var neighbours = top.Neighbours;
            double min = neighbours.Min(t => t.Value);
            chippest = neighbours.Find(t => min == t.Value
                    && t.IsVisited && IsRightNeighbour(t));       
            return chippest;
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

        public bool DestinationFound { get; set; }

        public PauseCycle Pause { get; set; }

        public virtual void ExtractNeighbours(IGraphTop button)
        {
            if (button is null)
                return;
            foreach (var neigbour in button.Neighbours)
            {
                if (neigbour.Value == 0 && !neigbour.IsStart)
                    neigbour.Value = button.Value + 1;
                queue.Enqueue(neigbour);
            }            
        }

        public virtual bool FindDestionation(IGraphTop start)
        {
            if (end == null)
                return false;
            watch.Start();
            var currentTop = start;
            Visit(currentTop);
            while (!IsDestination(currentTop))
            {
                currentTop = queue.Dequeue();
                if (IsRightCellToVisit(currentTop))
                {
                    //currentTop.Neighbours.Shuffle();
                    Visit(currentTop);                    
                }
                Pause(10);              
            }
            watch.Stop();
            return end.IsVisited;          
        }

        public void DrawPath(IGraphTop end)
        {
            var top = end;
            while (IsRightPath(top))
            {
                top = GoChippestNeighbour(top);
                if (top.IsSimpleTop)
                    top.MarkAsPath();
                steps++;
                //Pause(250);
            }
        }

        public bool IsDestination(IGraphTop button)
        {
            if (button is null)
                return false;
            return button.IsEnd || queue.IsEmpty();
        }

        public void Visit(IGraphTop top)
        {          
            if (top.IsObstacle)
                return;
            top.IsVisited = true;
            if (top.IsSimpleTop)
                top.MarkAsVisited();
            cellsVisited++;
            ExtractNeighbours(top);
        }

        public string GetStatistics()
        {
            return "Steps: " + steps.ToString() + "\n" +
                "Cells visited: " + cellsVisited.ToString();
        }

        public int Time => watch.Elapsed.Seconds + watch.Elapsed.Minutes * 60;
    }
}
