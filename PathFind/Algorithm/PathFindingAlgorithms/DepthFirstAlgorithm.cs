using System;
using System.Linq;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.ComponentModel;
using Algorithm.Extensions;
using Algorithm.EventArguments;
using Algorithm.PathFindingAlgorithms.Interface;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using Algorithm.Сalculations;
using Algorithm.Delegates;

namespace Algorithm.PathFindingAlgorithms
{
    /// <summary>
    /// Greedy algorithm. Each step looks for the best vertex and visits it
    /// </summary>
    [Description("Depth-first algorithm")]
    internal class DepthFirstAlgorithm : IPathFindingAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;
        public event Action<IVertex> OnVertexEnqueued;

        /// <summary>
        /// A function that selects the best vertex on the step
        /// </summary>
        public Func<IVertex, double> GreedyFunction { private get; set; }

        public IGraph Graph { get; protected set; }

        public bool IsDefault => false;

        public DepthFirstAlgorithm(IGraph graph)
        {
            GreedyFunction = vertex => DistanceCalculator.
                CalculateChebyshevDistance(vertex, graph.Start);
            Graph = graph;
            visitedVerticesStack = new Stack<IVertex>();
        }

        public void FindPath()
        {
            OnStarted?.Invoke(this, new AlgorithmEventArgs(Graph));
            var currentVertex = Graph.Start;
            currentVertex.IsVisited = true;
            while (!currentVertex.IsEnd)
            {
                var temp = currentVertex;
                currentVertex = GetNextVertex(currentVertex);
                if (!currentVertex.IsIsolated())
                {
                    currentVertex.IsVisited = true;
                    OnVertexVisited?.Invoke(currentVertex);
                    visitedVerticesStack.Push(currentVertex);
                    currentVertex.ParentVertex = temp;
                }
                else
                {
                    currentVertex = visitedVerticesStack.PopOrDefault();
                }
            }
            visitedVerticesStack.Clear();
            OnFinished?.Invoke(this, new AlgorithmEventArgs(Graph));
        }

        private IVertex GetNextVertex(IVertex vertex)
        {
            var neighbours = vertex.GetUnvisitedNeighbours().ToList();

            foreach (var vert in neighbours)
            {
                OnVertexEnqueued?.Invoke(vert);
            }

            return neighbours.FindOrDefault(vert =>
            {
                return GreedyFunction(vert) == neighbours.Min(GreedyFunction);
            });
        }

        private readonly Stack<IVertex> visitedVerticesStack;
    }
}
