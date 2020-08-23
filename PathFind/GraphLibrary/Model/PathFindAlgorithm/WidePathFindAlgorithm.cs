using GraphLibrary.Constants;
using GraphLibrary.Graph;
using GraphLibrary.PauseMaker;
using GraphLibrary.Statistics;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Algorithm
{
    /// <summary>
    /// A wave algorithm (Li algorithm, or wide path find algorithm). 
    /// Uses queue to move next graph top. Finds the shortest path to
    /// the destination top
    /// </summary>
    public class WidePathFindAlgorithm : IPathFindAlgorithm
    {
        protected Pauser pauseMaker;
        protected Queue<IVertex> neighbourQueue;
        public IStatisticsCollector StatCollector { get; set; }
        public Pause PauseEvent
        {
            get { return pauseMaker?.PauseEvent; }
            set { pauseMaker.PauseEvent = value; }
        }

        protected readonly AbstractGraph graph;

        public WidePathFindAlgorithm(AbstractGraph graph)
        {
            neighbourQueue = new Queue<IVertex>();
            this.graph = graph;
            StatCollector = new StatisticsCollector();
            pauseMaker = new Pauser();
        }

        private IVertex GoNextWave(IVertex vertex)
        {
            double min = vertex.Neighbours.Min(vert => vert.Value);
            return vertex.Neighbours.Find(vert => min == vert.Value
                    && vert.IsVisited && IsRightNeighbour(vert));
        }

        protected virtual double WaveFunction(IVertex vertex) => vertex.Value + 1;

        private bool IsRightNeighbour(IVertex vertex) => !vertex.IsEnd;

        private bool IsRightPath(IVertex vertex) => !vertex.IsStart;

        private bool IsRightCellToVisit(IVertex vertex) => !vertex.IsVisited;

        protected virtual bool IsSuitableForQueuing(IVertex vertex) => !vertex.IsVisited;

        private void AddToQueue(List<IVertex> neighbours)
        {
            foreach (var neighbour in neighbours)
            {
                if (IsSuitableForQueuing(neighbour))
                    neighbourQueue.Enqueue(neighbour);
            }
        }

        private void MakeWaves(IVertex vertex)
        {
            if (vertex is null)
                return;
            foreach (var neighbour in vertex.Neighbours)
            {
                if (!neighbour.IsVisited)
                    neighbour.Value = WaveFunction(vertex);
            }
        }

        public bool FindDestionation()
        {
            if (graph.End == null)
                return false;
            StatCollector.StartCollect();
            var currentVertex = graph.Start;
            Visit(currentVertex);
            MakeWaves(currentVertex);
            AddToQueue(currentVertex.Neighbours);
            while (!IsDestination(currentVertex))
            {
                currentVertex = neighbourQueue.Dequeue();
                if (IsRightCellToVisit(currentVertex))
                {
                    Visit(currentVertex);
                    MakeWaves(currentVertex);
                    AddToQueue(currentVertex.Neighbours);
                }
                pauseMaker?.Pause(Const.FIND_PROCESS_PAUSE_MILLISECONDS);
            }
            StatCollector.StopCollect();
            return graph.End.IsVisited;
        }

        public void DrawPath()
        {
            var vertex = graph.End;
            while (IsRightPath(vertex))
            {
                vertex = GoNextWave(vertex);
                if (vertex.IsSimpleVertex)
                    vertex.MarkAsPath();
                StatCollector.IncludeVertexInStatistics(vertex);
                pauseMaker?.Pause(Const.PATH_DRAW_PAUSE_MILLISECONDS);
            }
        }

        private bool IsDestination(IVertex vertex) => vertex is null ? false : vertex.IsEnd || !neighbourQueue.Any();

        private void Visit(IVertex vertex)
        {          
            if (vertex.IsObstacle)
                return;
            vertex.IsVisited = true;
            if (vertex.IsSimpleVertex)
            {
                vertex.MarkAsCurrentlyLooked();
                pauseMaker?.Pause(Const.VISIT_PAUSE_MILLISECONDS);
                vertex.MarkAsVisited();
            }
            StatCollector.Visited();
        }
    }
}
