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

        public WideSearch(IGraphTop end)
        {
            this.end = end;
        }

        private bool IsRigthCellToColor(IGraphTop top)
        {
            return !top.IsStart && !top.IsEnd;
        }

        public IGraphTop GoChippestNeighbour(IGraphTop top)
        {
            var chippest = top;
            var neighbours = top.Neighbours;
            int min = neighbours.Min(t => t.Value);
            chippest = neighbours.Find(t => min == t.Value
                    && t.IsVisited && IsRightNeighbour(t));       
            if (IsRigthCellToColor(chippest))
                chippest.MarkAsPath();
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

        public virtual void FindDestionation(IGraphTop start)
        {         
            watch.Start();
            var currentTop = start;
            Visit(currentTop);
            while (!IsDestination(currentTop))
            {
                currentTop = queue.Dequeue();
                if (IsRightCellToVisit(currentTop))
                {
                    currentTop.Neighbours.Shuffle();
                    Visit(currentTop);                    
                }
                Pause(10);              
            }
            watch.Stop();
            DestinationFound = queue.IsEmpty()
                && !currentTop.IsEnd ? false : true;            
        }

        public void DrawPath(IGraphTop end)
        {
            var top = end;
            while (IsRightPath(top))
            {
                top = GoChippestNeighbour(top);
                Pause(250);
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
            if (IsRigthCellToColor(top))
                top.MarkAsVisited();
            ExtractNeighbours(top);
        }

        public int GetTime()
        {
            return watch.Elapsed.Seconds + watch.Elapsed.Minutes * 60;
        }

        public bool CanStartSearch()
        {
            return end != null;
        }
    }
}
