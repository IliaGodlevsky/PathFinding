using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SearchAlgorythms.ButtonExtension;
using SearchAlgorythms.Extensions.QueueExtension;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class GreedySearch : ISearchAlgorythm
    {
        private Queue<GraphTop> queue =
            new Queue<GraphTop>();

        private readonly GraphTop end;

        private Stopwatch watch = new Stopwatch();

        protected void Pause(int value = 0)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        public GreedySearch(GraphTop end)
        {
            this.end = end;
        }

        private GraphTop GoChippestNeighbour(GraphTop top)
        {
            int min = 0;
            var neighbours = top.GetNeighbours();
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

        public void DrawPath(GraphTop end)
        {
            var top = end;
            while (!top.IsStart)
            {
                top = top.ParentTop;
                if (top.IsSimpleTop())
                    top.MarkAsPath();
                Pause(250);
            }
        }

        public void ExtractNeighbours(Button button)
        {
            return;
        }

        public void FindDestionation(GraphTop start)
        {
            watch.Start();
            var currentTop = start;
            GraphTop temp = null;
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
                    currentTop = queue.Dequeue();  
                Pause(10);
            }
            watch.Stop();
            DestinationFound = end.IsVisited;
        }

        public int GetTime()
        {
            return watch.Elapsed.Seconds + watch.Elapsed.Minutes * 60;
        }

        public bool IsDestination(Button button)
        {
            var top = button as GraphTop;
            return top.IsEnd && top.IsVisited || queue.IsEmpty();
        }

        public bool IsRightCellToVisit(Button button)
        {
            var top = button as GraphTop;
            if (top == null &&
                top.IsObstacle())
                return false;
            return true;
        }

        public void Visit(Button button)
        {
            var top = button as GraphTop;
            top.IsVisited = true;
            queue.Enqueue(top);
            if (top.IsSimpleTop())
                top.MarkAsVisited();
        }

        public bool CanStartSearch()
        {
            return end != null;
        }
    }
}
