using System.Collections.Generic;
using SearchAlgorythms.DelegatedMethods;
using SearchAlgorythms.Extensions;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class BestFirstAlgorithm : WideSearchAlgorithm
    {
        private Queue<IGraphTop> waveQueue = new Queue<IGraphTop>();

        public BestFirstAlgorithm(IGraphTop end) : base(end)
        {
            
        }

        private void MakeWavesFromEnd(IGraphTop end)
        {
            var top = end;
            MarkTop(top);
            while (!top.IsStart && !waveQueue.IsEmpty())
            {
                top = waveQueue.Dequeue();
                MarkTop(top);
            }
        }

        public override bool IsRightNeighbour(IGraphTop top)
        {
            return !top.IsStart;
        }

        public override bool IsRightPath(IGraphTop top)
        {
            return !top.IsEnd;
        }

        public override bool IsRightCellToVisit(IGraphTop button)
        {
            return (base.IsRightCellToVisit(button) && button.Value != 0) || button.IsEnd;
        }

        private void MarkTop(IGraphTop top)
        {
            if (top is null)
                return;
            foreach (var neigbour in top.Neighbours)
            {
                if (neigbour.Value == 0 && !neigbour.IsEnd)
                {
                    neigbour.Value = top.Value + 
                        DelegatedMethod.GetChebyshevDistance(neigbour, end);
                    waveQueue.Enqueue(neigbour);
                }
            }
        }

        public override void ExtractNeighbours(IGraphTop button)
        {
            if (button is null)
                return;
            foreach (var neigbour in button.Neighbours)
                if (!neigbour.IsVisited)
                    queue.Enqueue(neigbour);
        }

        public override bool FindDestionation(IGraphTop start)
        {
            MakeWavesFromEnd(end);
            bool found = base.FindDestionation(start);
            end = start;
            return found;
        }
    }
}
