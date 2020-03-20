using System.Collections.Generic;
using SearchAlgorythms.Extensions;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public class BestFirstSearch : WideSearch
    {
        private Queue<IGraphTop> waveQueue = new Queue<IGraphTop>();
        private readonly IGraphTop end;

        public BestFirstSearch(IGraphTop end) : base(end)
        {
            this.end = end;
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
            GraphTop top = button as GraphTop;
            return (base.IsRightCellToVisit(top) && top?.Value != 0) || top?.IsEnd == true;
        }

        private void MarkTop(IGraphTop top)
        {
            var currentTop = top as GraphTop;
            if (currentTop is null)
                return;
            var neigbours = currentTop.Neighbours;
            foreach (var neigbour in neigbours)
            {
                if (neigbour.Value == 0 && !neigbour.IsEnd)
                {
                    neigbour.Value = currentTop.Value + 1;
                    waveQueue.Enqueue(neigbour);
                }
            }
        }

        public override void ExtractNeighbours(IGraphTop button)
        {
            var currentTop = button as GraphTop;
            if (currentTop is null)
                return;
            var neighbours = currentTop.Neighbours;
            foreach (var neigbour in neighbours)
                queue.Enqueue(neigbour);
        }

        public override void FindDestionation(IGraphTop start)
        {
            MakeWavesFromEnd(end);
            base.FindDestionation(start);
        }
    }
}
