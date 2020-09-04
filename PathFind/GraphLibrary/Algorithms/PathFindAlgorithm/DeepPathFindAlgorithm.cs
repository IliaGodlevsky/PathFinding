using GraphLibrary.Algorithm;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.PauseMaker;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.PathFindAlgorithm
{
    public class DeepPathFindAlgorithm : IPathFindAlgorithm
    {        
        public Graph Graph { get; set; }
        public IPauseProvider Pauser { get; set; }

        public DeepPathFindAlgorithm()
        {
            visitedVerticesStack = new Stack<IVertex>();
        }

        public void FindDestionation()
        {
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
        }

        protected virtual IVertex GoNextVertex(IVertex vertex)
        {
            return vertex.GetUnvisitedNeighbours().Shuffle().FirstOrDefault();
        }

        private bool IsDestination(IVertex vertex)
        {
            return vertex.IsEnd || !visitedVerticesStack.Any()
                && !vertex.GetUnvisitedNeighbours().Any() || Graph.End == null;
        }

        private bool IsRightVertexToVisit(IVertex vertex)
        {
            return vertex != null || vertex?.IsObstacle == false;
        }

        private readonly Stack<IVertex> visitedVerticesStack;
    }
}
