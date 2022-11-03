using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    public abstract class GreedyAlgorithm : RangePathfindingAlgorithm
    {
        private readonly Stack<IVertex> visitedVerticesStack;

        private IVertex PreviousVertex { get; set; } = NullVertex.Instance;

        protected GreedyAlgorithm(IEndPoints endPoints)
           : base(endPoints)
        {
            visitedVerticesStack = new Stack<IVertex>();
        }

        protected abstract double GreedyHeuristic(IVertex vertex);

        protected override IVertex GetNextVertex()
        {
            var neighbours = GetUnvisitedVertices(CurrentVertex);
            double leastVertexCost = neighbours.Any() ? neighbours.Min(GreedyHeuristic) : default;
            var next = neighbours
                .ForEach(Enqueued)
                .FirstOrNullVertex(vertex => GreedyHeuristic(vertex) == leastVertexCost);
            return next.Neighbours.Count == 0 ? visitedVerticesStack.PopOrDeadEndVertex() : next;
        }

        protected override void Reset()
        {
            base.Reset();
            visitedVerticesStack.Clear();
        }

        protected override void VisitVertex(IVertex vertex)
        {
            visitedVertices.Visit(vertex);
            RaiseVertexVisited(new AlgorithmEventArgs(vertex));
            visitedVerticesStack.Push(vertex);
            parentVertices.Add(vertex, PreviousVertex);
        }

        protected override void InspectVertex(IVertex vertex)
        {
            PreviousVertex = vertex;
        }
    }
}