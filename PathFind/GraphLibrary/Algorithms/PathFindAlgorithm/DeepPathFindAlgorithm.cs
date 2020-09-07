using GraphLibrary.Algorithm;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;
using System;
using GraphLibrary.AlgorithmArgs;

namespace GraphLibrary.PathFindAlgorithm
{
    public class DeepPathFindAlgorithm : IPathFindingAlgorithm
    {
        public event AlgorithmEventHanlder OnAlgorithmStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnAlgorithmFinished;

        public Graph Graph { get; set; }

        public DeepPathFindAlgorithm()
        {
            visitedVerticesStack = new Stack<IVertex>();
        }

        public IEnumerable<IVertex> FindDestionation()
        {
            OnAlgorithmStarted?.Invoke(this, 
                new AlgorithmEventArgs(Graph));
            var currentVertex = Graph.Start;
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
            OnAlgorithmFinished?.Invoke(this, 
                new AlgorithmEventArgs(Graph));
            return this.GetFoundPath();
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
