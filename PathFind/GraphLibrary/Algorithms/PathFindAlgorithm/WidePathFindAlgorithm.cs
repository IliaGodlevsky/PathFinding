using GraphLibrary.Common.Constants;
using GraphLibrary.Extensions;
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
            double min = vertex.Neighbours.Min(vert => vert.AccumulatedCost);
            return vertex.Neighbours.Find(vert => min == vert.AccumulatedCost
                    && vert.IsVisited && IsRightNeighbour(vert));
        }

        protected virtual double WaveFunction(IVertex vertex) => vertex.AccumulatedCost + 1;

        private bool IsRightNeighbour(IVertex vertex) => !vertex.IsEnd;

        private bool IsRightPath(IVertex vertex) => !vertex.IsStart;

        private bool IsRightVertexToVisit(IVertex vertex) => !vertex.IsVisited;

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
                    neighbour.AccumulatedCost = WaveFunction(vertex);
            }
        }

        private void FindAction(IVertex currentVertex)
        {
            Visit(currentVertex);
            MakeWaves(currentVertex);
            AddToQueue(currentVertex.Neighbours);
        }

        public bool FindDestionation()
        {
            if (graph.End == null)
                return false;
            StatCollector.StartCollect();
            var currentVertex = graph.Start;
            FindAction(currentVertex);
            while (!IsDestination(currentVertex))
            {
                currentVertex = neighbourQueue.Dequeue();
                if (IsRightVertexToVisit(currentVertex))
                    FindAction(currentVertex);
                pauseMaker?.Pause(AlgorithmExecutionDelay.FIND_PROCESS_PAUSE);
            }
            StatCollector.StopCollect();
            return graph.End?.IsVisited == true;
        }

        public void DrawPath()
        {
            var vertex = graph.End;
            while (IsRightPath(vertex))
            {
                vertex = GoNextWave(vertex);
                if (vertex.IsSimpleVertex())
                    vertex.MarkAsPath();
                StatCollector.IncludeVertexInStatistics(vertex);
                pauseMaker?.Pause(AlgorithmExecutionDelay.PATH_DRAW_PAUSE);
            }
        }

        private bool IsDestination(IVertex vertex)
        {
            return !(vertex is null) && (vertex.IsEnd || !neighbourQueue.Any() || graph.End == null);
        }

        private void Visit(IVertex vertex)
        {          
            if (vertex.IsObstacle)
                return;
            vertex.IsVisited = true;
            if (vertex.IsSimpleVertex())
            {
                vertex.MarkAsCurrentlyLooked();
                pauseMaker?.Pause(AlgorithmExecutionDelay.VISIT_PAUSE);
                vertex.MarkAsVisited();
            }
            StatCollector.Visited();
        }
    }
}
