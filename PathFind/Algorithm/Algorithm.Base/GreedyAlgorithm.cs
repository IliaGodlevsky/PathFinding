using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    /// <summary>
    /// A base class for all Greedy algorithms.
    /// This is an abstract class
    /// </summary>
    public abstract class GreedyAlgorithm : Algorithm
    {
        protected GreedyAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
           : base(graph, endPoints)
        {
            visitedVerticesStack = new Stack<IVertex>();
        }

        public override sealed IGraphPath FindPath()
        {
            PrepareForPathfinding();
            while (!IsDestination(endPoints))
            {
                PreviousVertex = CurrentVertex;
                CurrentVertex = NextVertex;
                ProcessCurrentVertex();
            }
            CompletePathfinding();

            return CreateGraphPath();
        }

        protected virtual IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, endPoints);
        }

        /// <summary>
        /// A greedy function (heuristic) for algorithm
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        protected abstract double GreedyHeuristic(IVertex vertex);

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            visitedVerticesStack.Clear();
        }

        protected override void PrepareForPathfinding()
        {
            base.PrepareForPathfinding();
            CurrentVertex = endPoints.Source;
            visitedVertices.Add(CurrentVertex);
            RaiseVertexVisited(new AlgorithmEventArgs(CurrentVertex));
            visitedVerticesStack.Push(CurrentVertex);
        }

        /// <summary>
        /// Gets next vertex according 
        /// to <see cref="GreedyHeuristic(IVertex)"/> function
        /// </summary>
        protected override IVertex NextVertex
        {
            get
            {
                var neighbours = visitedVertices
                    .GetUnvisitedNeighbours(CurrentVertex)
                    .FilterObstacles();

                bool IsLeastCostVertex(IVertex vertex)
                {
                    return GreedyHeuristic(vertex) == neighbours.Min(GreedyHeuristic);
                }

                return neighbours
                    .ForEach(Enqueue)
                    .ToList()
                    .FindOrNullVertex(IsLeastCostVertex);
            }
        }

        private void VisitCurrentVertex()
        {
            visitedVertices.Add(CurrentVertex);
            RaiseVertexVisited(new AlgorithmEventArgs(CurrentVertex));
            visitedVerticesStack.Push(CurrentVertex);
            parentVertices.Add(CurrentVertex, PreviousVertex);
        }

        private void Enqueue(IVertex vertex)
        {
            RaiseVertexEnqueued(new AlgorithmEventArgs(vertex));
        }

        private void ProcessCurrentVertex()
        {
            if (CurrentVertex.IsNull())
            {
                CurrentVertex = visitedVerticesStack.PopOrNullVertex();
            }
            else
            {
                VisitCurrentVertex();
            }
        }

        private IVertex PreviousVertex { get; set; }

        private readonly Stack<IVertex> visitedVerticesStack;
    }
}