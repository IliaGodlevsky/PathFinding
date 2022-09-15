using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Algorithm.Realizations.GraphPaths;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    public abstract class GreedyAlgorithm : PathfindingAlgorithm
    {
        private readonly Stack<IVertex> visitedVerticesStack;

        private IVertex PreviousVertex { get; set; }

        protected GreedyAlgorithm(IEndPoints endPoints)
           : base(endPoints)
        {
            visitedVerticesStack = new Stack<IVertex>();
        }

        public sealed override IGraphPath FindPath()
        {
            PrepareForPathfinding();
            while (!IsDestination(endPoints))
            {
                WaitUntilResumed();
                PreviousVertex = CurrentVertex;
                CurrentVertex = GetNextVertex();
                ProcessCurrentVertex();
                ThrowIfDeadEnd(CurrentVertex);
            }
            CompletePathfinding();

            return CreateGraphPath();
        }

        protected virtual IGraphPath CreateGraphPath()
        {
            return IsInterruptRequested
                ? NullGraphPath.Interface
                : new GraphPath(parentVertices, endPoints);
        }

        protected abstract double GreedyHeuristic(IVertex vertex);

        protected override IVertex GetNextVertex()
        {
            var neighbours = GetUnvisitedVertices(CurrentVertex);
            double leastVertexCost = neighbours.Any() ? neighbours.Min(GreedyHeuristic) : default;
            return neighbours
                .ForEach(Enqueue)
                .FirstOrNullVertex(vertex => GreedyHeuristic(vertex) == leastVertexCost);
        }

        protected override void Reset()
        {
            base.Reset();
            visitedVerticesStack.Clear();
        }

        protected override void PrepareForPathfinding()
        {
            base.PrepareForPathfinding();
            CurrentVertex = endPoints.Source;
            ThrowIfDeadEnd(CurrentVertex);
            VisitVertex(CurrentVertex);
        }

        private void VisitCurrentVertex()
        {
            VisitVertex(CurrentVertex);
            parentVertices.Add(CurrentVertex, PreviousVertex);
        }

        private void VisitVertex(IVertex vertex)
        {
            visitedVertices.Visit(vertex);
            RaiseVertexVisited(new AlgorithmEventArgs(vertex));
            visitedVerticesStack.Push(vertex);
        }

        private void Enqueue(IVertex vertex)
        {
            RaiseVertexEnqueued(new AlgorithmEventArgs(vertex));
        }

        private void ProcessCurrentVertex()
        {
            if (CurrentVertex.IsNull() && visitedVerticesStack.Count > 0)
            {
                CurrentVertex = visitedVerticesStack.Pop();
            }
            else
            {
                VisitCurrentVertex();
            }
        }
    }
}