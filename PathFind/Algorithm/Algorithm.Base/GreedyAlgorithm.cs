using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Common.Disposables;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
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

        protected sealed override IGraphPath FindPathImpl()
        {
            PrepareForPathfinding();

            using (Disposable.Use(CompletePathfinding))
            {
                while (!IsDestination(endPoints))
                {
                    ThrowIfInterrupted();
                    WaitUntilResumed();
                    PreviousVertex = CurrentVertex;
                    CurrentVertex = GetNextVertex();
                    ProcessCurrentVertex();
                }
            }

            return CreateGraphPath();
        }

        protected virtual IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, endPoints);
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
            if (CurrentVertex.Neighbours.Count == 0)
            {
                CurrentVertex = visitedVerticesStack.PopOrDeadEndVertex();
            }
            else
            {
                VisitCurrentVertex();
            }
        }
    }
}