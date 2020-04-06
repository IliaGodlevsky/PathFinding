using System.Collections.Generic;
using System.Linq;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    /// <summary>
    /// An euristic Li algorithm. Ignors tops that are far from destination top
    /// </summary>
    public class BestFirstAlgorithm : WidePathFindAlgorithm
    {
        private Queue<IVertex> waveQueue = new Queue<IVertex>();

        public BestFirstAlgorithm(AbstractGraph graph) : base(graph)
        {
            
        }

        private void MakeWavesFromEnd(IVertex end)
        {
            var vertex = end;
            MarkTop(vertex);
            while (!vertex.IsStart && waveQueue.Any())
            {
                vertex = waveQueue.Dequeue();
                MarkTop(vertex);
            }
        }

        public override bool IsRightNeighbour(IVertex vertex)
        {
            return !vertex.IsStart;
        }

        public override bool IsRightPath(IVertex vertex)
        {
            return !vertex.IsEnd;
        }

        public override bool IsRightCellToVisit(IVertex vertex)
        {
            return base.IsRightCellToVisit(vertex) && vertex.Value > 0;
        }

        private void MarkTop(IVertex vertex)
        {
            if (vertex is null)
                return;
            foreach (var neigbour in vertex.Neighbours)
            {
                if (neigbour.Value == 0)
                {
                    neigbour.Value = vertex.Value + 1;
                    waveQueue.Enqueue(neigbour);
                }
            }
        }

        public override void ExtractNeighbours(IVertex vertex)
        {
            if (vertex is null)
                return;
            foreach (var neigbour in vertex.Neighbours)
                if (!neigbour.IsVisited)
                    neighbourQueue.Enqueue(neigbour);
        }

        public override bool FindDestionation()
        {
            graph.End.Value = 1;
            MakeWavesFromEnd(graph.End);           
            bool found = base.FindDestionation();
            graph.End = graph.Start;
            return found;
        }
    }
}
