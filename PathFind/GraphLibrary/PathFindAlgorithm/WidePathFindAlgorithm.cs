using System.Collections.Generic;
using System.Linq;
using SearchAlgorythms.Top;
using SearchAlgorythms.Extensions.ListExtensions;
using SearchAlgorythms.Statistics;
using SearchAlgorythms.Graph;

namespace SearchAlgorythms.Algorithm
{
    /// <summary>
    /// A wave algorithm (Li algorithm, or wide path find algorithm). 
    /// Uses queue to move next graph top. Finds the shortest path to
    /// the destination top
    /// </summary>
    public class WidePathFindAlgorithm : IPathFindAlgorithm
    {
        protected Queue<IVertex> neighbourQueue = new Queue<IVertex>();
        private UnweightedGraphSearchAlgoStatistics statCollector;
        protected readonly AbstractGraph graph;


        public WidePathFindAlgorithm(AbstractGraph graph)
        {
            this.graph = graph;
            statCollector = new UnweightedGraphSearchAlgoStatistics();
        }

        public IVertex GoChippestNeighbour(IVertex vertex)
        {
            vertex.Neighbours.Shuffle();
            double min = vertex.Neighbours.Min(vert => vert.Value);
            return vertex.Neighbours.Find(vert => min == vert.Value
                    && vert.IsVisited && IsRightNeighbour(vert));
        }

        public virtual bool IsRightNeighbour(IVertex vertex)
        {
            return !vertex.IsEnd;
        }

        public virtual bool IsRightPath(IVertex vertex)
        {
            return !vertex.IsStart;
        }

        public virtual bool IsRightCellToVisit(IVertex vertex)
        {
            return !vertex.IsVisited;
        }

        public PauseCycle Pause { get; set; }

        public virtual void ExtractNeighbours(IVertex vertex)
        {
            if (vertex is null)
                return;
            foreach (var neigbour in vertex.Neighbours)
            {
                if (neigbour.Value == 0 && !neigbour.IsStart)
                    neigbour.Value = vertex.Value + 1;
                if (!neigbour.IsVisited)
                    neighbourQueue.Enqueue(neigbour);
            }
        }

        public virtual bool FindDestionation()
        {
            if (graph.End == null)
                return false;
            statCollector.BeginCollectStatistic();
            var currentVertex = graph.Start;
            Visit(currentVertex);
            while (!IsDestination(currentVertex))
            {
                currentVertex = neighbourQueue.Dequeue();
                if (IsRightCellToVisit(currentVertex))
                    Visit(currentVertex);
                Pause(2);              
            }
            statCollector.StopCollectStatistics();
            return graph.End.IsVisited;          
        }

        public void DrawPath()
        {
            var vertex = graph.End;
            while (IsRightPath(vertex))
            {
                vertex = GoChippestNeighbour(vertex);
                if (vertex.IsSimpleVertex)
                    vertex.MarkAsPath();
                statCollector.AddStep();
                Pause(35);
            }
        }

        private bool IsDestination(IVertex vertex)
        {
            if (vertex is null)
                return false;
            return vertex.IsEnd || !neighbourQueue.Any();
        }

        private void Visit(IVertex vertex)
        {          
            if (vertex.IsObstacle)
                return;
            vertex.IsVisited = true;
            if (vertex.IsSimpleVertex)
            {
                vertex.MarkAsCurrentlyLooked();
                Pause(8);
                vertex.MarkAsVisited();
            }
            statCollector.CellVisited();
            ExtractNeighbours(vertex);
        }

        public string GetStatistics()
        {
            return statCollector.Statistics;
        }
    }
}
