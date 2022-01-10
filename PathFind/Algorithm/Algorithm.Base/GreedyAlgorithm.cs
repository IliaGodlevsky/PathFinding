using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Algorithm.Realizations.GraphPaths;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using Interruptable.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    /// <summary>
    /// A base class for all Greedy algorithms.
    /// This is an abstract class
    /// </summary>
    public abstract class GreedyAlgorithm : PathfindingAlgorithm,
        IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        protected GreedyAlgorithm(IEndPoints endPoints)
           : base(endPoints)
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
            return IsTerminatedPrematurely
                ? new GraphPath(parentVertices, endPoints)
                : NullGraphPath.Instance;
        }

        /// <summary>
        /// A greedy function (heuristic) for algorithm
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        protected abstract double GreedyHeuristic(IVertex vertex);

        protected override void Reset()
        {
            base.Reset();
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
                    .FilterObstacles()
                    .ToArray();
                double leastVertexCost = neighbours.MinOrDefault(GreedyHeuristic);
                bool IsLeastCostVertex(IVertex vertex)
                    => GreedyHeuristic(vertex) == leastVertexCost;
                return neighbours
                    .ForAll(Enqueue)
                    .FirstOrNullVertex(IsLeastCostVertex);
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