using GraphLibrary.Graphs;
using System.Collections.Generic;
using System.Linq;
using System;
using GraphLibrary.EventArguments;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.Extensions.CustomTypeExtensions;

namespace GraphLibrary.PathFindingAlgorithm
{
    public class DeepAlgorithm : IPathFindingAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;

        public IGraph Graph { get; set; }

        public DeepAlgorithm()
        {
            Graph = NullGraph.Instance;
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
                    currentVertex.IsVisited = true;
                    OnVertexVisited?.Invoke(currentVertex);
                    visitedVerticesStack.Push(currentVertex);
                    currentVertex.ParentVertex = temp;
                }
                else
                    currentVertex = visitedVerticesStack.PopOrNullVertex();
            }
            OnFinished?.Invoke(this, new AlgorithmEventArgs(Graph));
            return this.GetFoundPath();
        }

        protected virtual IVertex GoNextVertex(IVertex vertex)
        {
            return vertex.GetUnvisitedNeighbours().ToList().FirstOrNullVertex();
        }

        private readonly Stack<IVertex> visitedVerticesStack;
    }
}
