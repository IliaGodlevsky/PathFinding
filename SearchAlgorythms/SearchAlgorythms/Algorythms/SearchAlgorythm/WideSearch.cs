using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class WideSearch : ISearchAlgorythm
    {
        private void Pause(int value = 0)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        private GraphTop GoChippestNeighbour(GraphTop top)
        {
            GraphTop temp = top;
            var neighbours = top.GetNeighbours();
            var start = neighbours.Find(f => f.IsStart);
            if (start != null)
                return start;
            for (int i = 0; i < neighbours.Count; i++) 
            {
                if (IsRightNeighbour(temp, neighbours[i]))
                    temp = neighbours[i];
            }
            temp.BackColor = Color.FromName("Cyan");
            return temp;
        }

        private bool IsRightNeighbour(GraphTop top1, GraphTop top2)
        {
            return top1.Value > top2.Value && top2.IsVisited && !top2.IsEnd;
        }

        public bool IsRightCell(Button button)
        {
            GraphTop top = button as GraphTop;
            return top is null ? false : !top.IsVisited;
        }

        private Queue<GraphTop> queue = new Queue<GraphTop>();

        public void ExtractNeighbours(Button button)
        {
            var top = button as GraphTop;
            if (top is null)
                return;
            foreach (var g in top.GetNeighbours())
            {
                if (g.Value == 0)
                    g.Value = top.Value + 1;
                queue.Enqueue(g);
            }
        }

        public void FindDestionation(GraphTop start)
        {
            Visit(start);
            var currentTop = queue.Dequeue();
            while(!IsDestination(currentTop))
            {
                if (IsRightCell(currentTop))
                    Visit(currentTop);
                currentTop = queue.Dequeue();
                Pause(10);              
            }
            Visit(currentTop);
        }

        public void DrawPath(GraphTop end)
        {
            GraphTop top = end;
            while (!top.IsStart)
                top = GoChippestNeighbour(top);            
        }

        public bool IsDestination(Button button)
        {
            var top = button as GraphTop;
            if (top is null)
                return false;
            return top.IsEnd;
        }

        public void Visit(Button button)
        {
            var top = button as GraphTop;
            if (top is null)
                return;
            top.IsVisited = true;
            if (!top.IsStart && !top.IsEnd)
                top.BackColor = Color.FromName("Yellow");
            ExtractNeighbours(top);
        }
    }
}
