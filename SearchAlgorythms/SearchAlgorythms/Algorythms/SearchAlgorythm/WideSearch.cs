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

        private Queue<GraphTop> queue = new Queue<GraphTop>();

        public void ExtractNeighbours(Button button)
        {
            var top = button as GraphTop;
            if (top is null)
                return;
            foreach (var g in top.GetNeighbours())
                queue.Enqueue(g);
        }

        public void FindDestionation(GraphTop start)
        {
            Visit(start);
            var currentTop = queue.Dequeue();
            while(!IsDestination(currentTop))
            {
                if (!currentTop.IsVisited 
                    && currentTop.BackColor != Color.FromName("Black"))
                    Visit(currentTop);
                currentTop = queue.Dequeue();
                Pause(10);              
            }
            MessageBox.Show("Destination is found");           
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
            top.IsVisited = true;
            if (!top.IsStart)
                top.BackColor = Color.FromName("Yellow");
            ExtractNeighbours(top);
        }
    }
}
