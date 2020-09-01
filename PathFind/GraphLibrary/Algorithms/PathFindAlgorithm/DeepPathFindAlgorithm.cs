using GraphLibrary.Algorithm;
using GraphLibrary.Common.Extensions;
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
        public IStatisticsCollector StatCollector { get; set; }
        public AbstractGraph Graph { get; set; }
        public IPauseProvider Pauser { get; set; }

        public DeepPathFindAlgorithm()
        {
            visitedVerticesStack = new Stack<IVertex>();
            StatCollector = new StatisticsCollector();           
        }

        public void DrawPath() => this.DrawPath(vertex => vertex.ParentVertex);

        public void FindDestionation()
        {
            if (this.IsRightGraphSettings())
            {
                StatCollector.StartCollect();
                var currentVertex = Graph.Start;
                this.VisitVertex(currentVertex);
                while (!IsDestination(currentVertex))
                {
                    var temp = currentVertex;
                    currentVertex = GoNextVertex(currentVertex);
                    if (IsRightVertexToVisit(currentVertex))
                    {
                        this.VisitVertex(currentVertex);
                        visitedVerticesStack.Push(currentVertex);
                        currentVertex.ParentVertex = temp;
                    }
                    else
                        currentVertex = visitedVerticesStack.Pop();
                }
                StatCollector.StopCollect();
            }
        }

        protected virtual IVertex GoNextVertex(IVertex vertex)
        {
            return vertex.Neighbours.Find(vert => !vert.IsVisited);
        }

        private bool IsDestination(IVertex vertex)
        {
            bool hasUnvisitedNeighbours = vertex.Neighbours.Find(v => !v.IsVisited) != null;
            bool hasReachedEnd = vertex.IsEnd && vertex.IsVisited;
            bool hasVerticesToComeBack = visitedVerticesStack.Any();
            bool lostDestinationPoint = Graph.End == null;

            return hasReachedEnd || !hasVerticesToComeBack
                && !hasUnvisitedNeighbours || lostDestinationPoint;
        }

        private bool IsRightVertexToVisit(IVertex vertex)
        {
            return vertex != null || vertex?.IsObstacle == false;
        }

        private readonly Stack<IVertex> visitedVerticesStack;
    }
}
