using System;
using System.Linq;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.PathFindingAlgorithm.Interface;
using System.Collections.Generic;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.EventArguments;
using GraphLibrary.DistanceCalculating;
using System.ComponentModel;

namespace GraphLibrary.PathFindingAlgorithm
{
    /// <summary>
    /// Greedy algorithm. Each step looks for the best vertex and visits it
    /// </summary>
    [Description("Depth-first algorithm")]
    public class DepthFirstAlgorithm : IPathFindingAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;
        public event Action<IVertex> OnEnqueued;

        /// <summary>
        /// A function that selects the best vertex on the step
        /// </summary>
        public Func<IVertex, double> GreedyFunction { private get; set; }

        public IGraph Graph { get; protected set; }

        public DepthFirstAlgorithm(IGraph graph)
        {
            GreedyFunction = vertex => DistanceCalculator.GetChebyshevDistance(vertex, graph.Start);
            Graph = graph;
            visitedVerticesStack = new Stack<IVertex>();
        }

        public void FindPath()
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
                    currentVertex = visitedVerticesStack.Pop();
            }
            OnFinished?.Invoke(this, new AlgorithmEventArgs(Graph));
        }

        private IVertex GoNextVertex(IVertex vertex)
        {
            var neighbours = vertex.GetUnvisitedNeighbours().ToList();
            foreach (var vert in neighbours)
                OnEnqueued?.Invoke(vert);
            return neighbours.FindOrNullVertex(vert =>
            {
                return GreedyFunction(vert) == neighbours.Min(GreedyFunction);
            });
        }

        private readonly Stack<IVertex> visitedVerticesStack;
    }
}
