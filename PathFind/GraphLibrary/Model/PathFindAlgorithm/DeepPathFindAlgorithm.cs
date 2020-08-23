using GraphLibrary.Algorithm;
using GraphLibrary.Constants;
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
                if (vertex.IsSimpleVertex)
                    vertex.MarkAsPath();
                StatCollector.IncludeVertexInStatistics(temp);
                pauseMaker?.Pause(Const.PATH_DRAW_PAUSE_MILLISECONDS);
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
                IVertex temp = currentVertex;
                currentVertex = GoNextVertex(currentVertex);
                if (IsRightCellToVisit(currentVertex))
                {
                    Visit(currentVertex);
                    currentVertex.ParentVertex = temp;
                }
                else
                    currentVertex = visitedVerticesStack.Pop();
                pauseMaker?.Pause(Const.FIND_PROCESS_PAUSE_MILLISECONDS);
            }
            StatCollector.StartCollect();
            return graph.End.IsVisited;
        }

        private bool IsDestination(IVertex vertex)
        {
            return vertex.IsEnd && vertex.IsVisited
                || (!visitedVerticesStack.Any() && vertex.Neighbours.Find(v => !v.IsVisited) == null);
        }

        private bool IsRightCellToVisit(IVertex vertex)
        {
            if (vertex == null)
                return false;
            if (vertex.IsObstacle)
                return false;
            return true;
        }

        private void Visit(IVertex vertex)
        {
            vertex.IsVisited = true;
            visitedVerticesStack.Push(vertex);
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
