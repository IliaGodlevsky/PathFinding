using GraphLibrary.Algorithm;
using GraphLibrary.Common.Constants;
using GraphLibrary.Extensions;
using GraphLibrary.Graph;
using GraphLibrary.PauseMaker;
using GraphLibrary.Statistics;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.PathFindAlgorithm
{
    public class DeepPathFindAlgorithm : IPathFindAlgorithm
    {
        protected Pauser pauseMaker;
        private readonly AbstractGraph graph;
        private readonly Stack<IVertex> visitedVerticesStack;
        public IStatisticsCollector StatCollector { get; set; }

        public DeepPathFindAlgorithm(AbstractGraph graph)
        {
            visitedVerticesStack = new Stack<IVertex>();
            StatCollector = new StatisticsCollector();
            this.graph = graph;
            pauseMaker = new Pauser();
        }

        protected virtual IVertex GoNextVertex(IVertex vertex)
        {
            return vertex.Neighbours.Find(vert => !vert.IsVisited);
        }

        public Pause PauseEvent
        {
            get { return pauseMaker?.PauseEvent; }
            set { pauseMaker.PauseEvent = value; }
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

        public bool FindDestionation()
        {
            if (graph.End == null)
                return false;
            StatCollector.StartCollect();
            var currentVertex = graph.Start;
            Visit(currentVertex);
            while (!IsDestination(currentVertex))
            {
                var temp = currentVertex;
                currentVertex = GoNextVertex(currentVertex);
                if (IsRightCellToVisit(currentVertex))
                {
                    Visit(currentVertex);
                    currentVertex.ParentVertex = temp;
                }
                else
                    currentVertex = visitedVerticesStack.Pop();
                pauseMaker?.Pause(AlgorithmExecutionDelay.FIND_PROCESS_PAUSE);
            }
            StatCollector.StartCollect();
            return graph.End?.IsVisited == true;
        }

        private bool IsDestination(IVertex vertex)
        {
            bool hasUnvisitedNeighbours = vertex.Neighbours.Find(v => !v.IsVisited) != null;
            bool hasReachedEnd = vertex.IsEnd && vertex.IsVisited;
            bool hasVerticesToComeBack = visitedVerticesStack.Any();
            bool lostDestinationPoint = graph.End == null;

            return hasReachedEnd
                || !hasVerticesToComeBack
                && !hasUnvisitedNeighbours
                || lostDestinationPoint;
        }

        private bool IsRightCellToVisit(IVertex vertex)
        {
            return vertex != null || vertex?.IsObstacle == false;
        }

        private void Visit(IVertex vertex)
        {
            vertex.IsVisited = true;
            visitedVerticesStack.Push(vertex);
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
