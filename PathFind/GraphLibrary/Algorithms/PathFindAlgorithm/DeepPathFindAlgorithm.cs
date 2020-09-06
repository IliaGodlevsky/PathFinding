using GraphLibrary.Algorithm;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GraphLibrary.PathFindAlgorithm
{
    public class DeepPathFindAlgorithm : IPathFindAlgorithm
    {
        public Graph Graph { get; set; }

        public DeepPathFindAlgorithm()
        {
            visitedVerticesStack = new Stack<IVertex>();
        }

        public void FindDestionation()
        {
            OnAlgorithmStarted?.Invoke();
            var currentVertex = Graph.Start;
            this.VisitVertex(currentVertex);
            while (!IsDestination(currentVertex))
            {
                var temp = currentVertex;
                currentVertex = GoNextVertex(currentVertex);
                if (IsRightVertexToVisit(currentVertex))
                {
                    OnVertexVisited?.Invoke(currentVertex);
                    visitedVerticesStack.Push(currentVertex);
                    currentVertex.ParentVertex = temp;
                }
                else
                    currentVertex = visitedVerticesStack.Pop();
            }
            OnAlgorithmFinished?.Invoke();
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

        public event AlgorithmEventHanlder OnAlgorithmStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnAlgorithmFinished;
    }
}
