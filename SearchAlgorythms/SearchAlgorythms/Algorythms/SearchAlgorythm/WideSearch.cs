using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SearchAlgorythms.ButtonExtension;
using SearchAlgorythms.Top;
using SearchAlgorythms.Extensions.ListExtensions;
using SearchAlgorythms.Extensions.QueueExtension;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class WideSearch : ISearchAlgorythm
    {
        private Stopwatch watch = new Stopwatch();

        protected void Pause(int value = 0)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        private bool IsRigthCellToColor(GraphTop top)
        {
            return !top.IsStart && !top.IsEnd;
        }

        public GraphTop GoChippestNeighbour(GraphTop top)
        {
            var chippest = top;
            var neighbours = top.GetNeighbours();
            int min = neighbours.Min(t => t.Value);
            chippest = neighbours.Find(t => min == t.Value
                    && t.IsVisited && IsRightNeighbour(t));       
            if (IsRigthCellToColor(chippest))
                chippest.MarkAsPath();
            return chippest;
        }

        public virtual bool IsRightNeighbour(GraphTop top2)
        {
            return !top2.IsEnd;
        }

        public virtual bool IsRightPath(GraphTop top)
        {
            return !top.IsStart;
        }

        public virtual bool IsRightCellToVisit(Button button)
        {
            var top = button as GraphTop;
            return top is null ? false : !top.IsVisited;
        }

        protected Queue<GraphTop> queue = new Queue<GraphTop>();

        public bool DestinationFound { get; set; }

        public virtual void ExtractNeighbours(Button button)
        {
            var currentTop = button as GraphTop;
            if (currentTop is null)
                return;
            foreach (var neigbour in currentTop.GetNeighbours())
            {
                if (neigbour.Value == 0 && !neigbour.IsStart)
                    neigbour.Value = currentTop.Value + 1;
                queue.Enqueue(neigbour);
            }            
        }

        public virtual void FindDestionation(GraphTop start)
        {         
            watch.Start();
            var currentTop = start;
            Visit(currentTop);
            while (!IsDestination(currentTop))
            {
                currentTop = queue.Dequeue();
                if (IsRightCellToVisit(currentTop))
                {
                    currentTop.GetNeighbours().Shuffle();
                    Visit(currentTop);                    
                }
                Pause(10);              
            }
            watch.Stop();
            DestinationFound = queue.IsEmpty()
                && !currentTop.IsEnd ? false : true;            
        }

        public void DrawPath(GraphTop end)
        {
            var top = end;
            while (IsRightPath(top))
            {
                top = GoChippestNeighbour(top);
                Pause(250);
            }
        }

        public bool IsDestination(Button button)
        {
            var top = button as GraphTop;
            if (top is null)
                return false;
            return top.IsEnd || queue.IsEmpty();
        }

        public void Visit(Button button)
        {          
            if (button.IsObstacle())
                return;
            var top = button as GraphTop;
            top.IsVisited = true;
            if (IsRigthCellToColor(top))
                top.MarkAsVisited();
            ExtractNeighbours(top);
        }

        public int GetTime()
        {
            return watch.Elapsed.Seconds + watch.Elapsed.Minutes * 60;
        }
    }
}
