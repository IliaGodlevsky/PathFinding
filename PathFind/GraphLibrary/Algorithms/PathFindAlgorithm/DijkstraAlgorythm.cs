using GraphLibrary.Common.Constants;
using GraphLibrary.Extensions;
using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Graph;
using GraphLibrary.PauseMaker;
using GraphLibrary.Statistics;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Algorithm
{
    /// <summary>
    /// Finds the chippest path to destination top. 
    /// </summary>
    public class DijkstraAlgorithm : IPathFindAlgorithm
    {
        protected Pauser pauseMaker;
        protected readonly AbstractGraph graph;
        private readonly List<IVertex> neigbourQueue;
        public IStatisticsCollector StatCollector { get; set; }
        public Pause PauseEvent 
        {
            get { return pauseMaker?.PauseEvent; }
            set { pauseMaker.PauseEvent = value; }
        }

        public DijkstraAlgorithm(AbstractGraph graph)
        {
            neigbourQueue = new List<IVertex>();
            this.graph = graph;
            IVertex SetValueToInfinity(IVertex vertex)
            {
                vertex.AccumulatedCost = double.PositiveInfinity; 
                return vertex;
            }
            this.graph.GetArray().Apply(SetValueToInfinity);
            StatCollector = new StatisticsCollector();
            pauseMaker = new Pauser();
        }

        public void DrawPath()
        {
            var vertex = graph.End;
            while (!vertex.IsStart)
            {
                var temp = vertex;
                vertex = vertex.ParentVertex;
                if (vertex.IsSimpleVertex())
                    vertex.MarkAsPath();
                StatCollector.IncludeVertexInStatistics(temp);
                pauseMaker?.Pause(AlgorithmExecutionDelay.PATH_DRAW_PAUSE);
            }
        }

        private IVertex GetChippestUnvisitedVertex()
        {
            neigbourQueue.RemoveAll(vertex => vertex.IsVisited);
            neigbourQueue.Sort((vertex1, vertex2) => vertex1.AccumulatedCost.CompareTo(vertex2.AccumulatedCost));
            return neigbourQueue.Any() ? neigbourQueue.First() : null;
        }

        protected virtual double GetPathValue(IVertex neighbour, IVertex vertex)
        {
            return neighbour.Cost + vertex.AccumulatedCost;
        }

        private void MakeWaves(IVertex vertex)
        {
            if (vertex is null)
                return;
            var neighbours = vertex.Neighbours;
            foreach(var neighbour in neighbours)
            {
                if (!neighbour.IsVisited)
                    neigbourQueue.Add(neighbour);
                if (neighbour.AccumulatedCost > GetPathValue(neighbour, vertex))
                {                    
                    neighbour.AccumulatedCost = GetPathValue(neighbour, vertex);
                    neighbour.ParentVertex = vertex;
                }
            }
        }

        public bool FindDestionation()
        {
            if (graph.End == null)
                return false;
            StatCollector.StartCollect();
            var currentVertex = graph.Start;
            currentVertex.IsVisited = true;
            currentVertex.AccumulatedCost = 0;
            do
            {
                MakeWaves(currentVertex);
                currentVertex = GetChippestUnvisitedVertex();
                if (!IsValidVertex(currentVertex)) 
                    break;
                if (IsRightVertexToVisit(currentVertex))
                    Visit(currentVertex);
                pauseMaker?.Pause(AlgorithmExecutionDelay.FIND_PROCESS_PAUSE);
            } while (!IsDestination(currentVertex));
            StatCollector.StopCollect();
            return graph.End?.IsVisited == true;
        }

        private bool IsValidVertex(IVertex vertex)
        {
            return vertex?.AccumulatedCost != double.PositiveInfinity && vertex != null;
        }

        private bool IsDestination(IVertex vertex)
        {
            if (vertex == null)
                return false;
            if (vertex.IsObstacle)
                return false;
            return vertex.IsEnd && vertex.IsVisited
                || graph.End == null;
        }

        private bool IsRightVertexToVisit(IVertex vertex)
        {
            if (vertex is null)
                return false;
            if (vertex.IsObstacle)
                return false;
            else
                return !vertex.IsVisited;
        }

        private void Visit(IVertex vertex)
        {
            vertex.IsVisited = true;
            if (!vertex.IsEnd)
            {
                vertex.MarkAsCurrentlyLooked();
                pauseMaker?.Pause(AlgorithmExecutionDelay.VISIT_PAUSE);
                vertex.MarkAsVisited();               
            }
            StatCollector.Visited();
        }
    }
}
