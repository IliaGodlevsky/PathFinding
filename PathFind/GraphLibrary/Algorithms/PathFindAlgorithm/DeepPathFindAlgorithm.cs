using GraphLibrary.Algorithm;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;
using System;
using GraphLibrary.AlgorithmArgs;
using GraphLibrary.Model.Vertex;
using GraphLibrary.Common.Extensions.CollectionExtensions;

namespace GraphLibrary.PathFindAlgorithm
{
    public class DeepPathFindAlgorithm : IPathFindingAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;

        public Graph Graph { get; set; }

        public DeepPathFindAlgorithm()
        {
            visitedVerticesStack = new Stack<IVertex>();
        }

        public IEnumerable<IVertex> FindPath()
        {
            OnStarted?.Invoke(this, new AlgorithmEventArgs(Graph));
            var currentVertex = Graph.Start;
            while (!currentVertex.IsEnd)
            {
                var temp = currentVertex;
                currentVertex = GoNextVertex(currentVertex);
                if (!currentVertex.IsIsolated())
                {
                    OnVertexVisited?.Invoke(currentVertex);
                    visitedVerticesStack.Push(currentVertex);
                    currentVertex.ParentVertex = temp;
                }
                else
                    currentVertex = visitedVerticesStack.PopSecure();
            }
            OnFinished?.Invoke(this, new AlgorithmEventArgs(Graph));
            return this.GetFoundPath();
        }

        protected virtual IVertex GoNextVertex(IVertex vertex)
        {
            if (vertex.GetUnvisitedNeighbours().Any())
                return vertex.GetUnvisitedNeighbours().Shuffle().FirstOrDefault();
            return NullVertex.GetInstance();
        }

        private readonly Stack<IVertex> visitedVerticesStack;
    }
}
