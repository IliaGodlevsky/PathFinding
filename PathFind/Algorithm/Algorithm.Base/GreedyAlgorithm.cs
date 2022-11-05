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
    public abstract class GreedyAlgorithm : PathfindingAlgorithm<Stack<IVertex>>
    {
        private IVertex PreviousVertex { get; set; } = NullVertex.Instance;

        protected GreedyAlgorithm(IEndPoints endPoints)
           : base(endPoints)
        {
        }

        protected abstract double GreedyHeuristic(IVertex vertex);

        protected override IVertex GetNextVertex()
        {
            var neighbours = GetUnvisitedNeighbours(CurrentVertex);
            double leastVertexCost = neighbours.Any() ? neighbours.Min(GreedyHeuristic) : default;
            return neighbours
                .ForEach(Enqueued)
                .FirstOrNullVertex(vertex => GreedyHeuristic(vertex) == leastVertexCost);
        }

        protected override void PrepareForSubPathfinding(Range range)
        {
            base.PrepareForSubPathfinding(range);
            VisitVertex(CurrentVertex);
        }

        private void VisitVertex(IVertex vertex)
        {
            visited.Add(vertex);
            RaiseVertexVisited(new AlgorithmEventArgs(vertex));
            storage.Push(vertex);
        }

        protected override void DropState()
        {
            base.DropState();
            storage.Clear();
        }

        protected override void VisitCurrentVertex()
        {
            if (CurrentVertex.HasNoNeighbours())
            {
                CurrentVertex = storage.PopOrDeadEndVertex();
            }
            else
            {
                VisitVertex(CurrentVertex);
                traces[CurrentVertex.Position] = PreviousVertex;
            }
        }

        protected override void InspectVertex(IVertex vertex)
        {
            PreviousVertex = vertex;
        }
    }
}