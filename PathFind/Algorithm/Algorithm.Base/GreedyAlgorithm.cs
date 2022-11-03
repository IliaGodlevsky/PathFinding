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
        private readonly Stack<IVertex> tracesFromDeadend;

        private IVertex PreviousVertex { get; set; } = NullVertex.Instance;

        protected GreedyAlgorithm(IEndPoints endPoints)
           : base(endPoints)
        {
            tracesFromDeadend = new Stack<IVertex>();
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

        protected override void Reset()
        {
            base.Reset();
            tracesFromDeadend.Clear();
        }

        protected override void VisitCurrentVertex()
        {
            if (CurrentVertex.Neighbours.Count == 0)
            {
                CurrentVertex = tracesFromDeadend.PopOrDeadEndVertex();
            }
            else
            {
                visited.Add(CurrentVertex);
                RaiseVertexVisited(new AlgorithmEventArgs(CurrentVertex));
                tracesFromDeadend.Push(CurrentVertex);
                traces[CurrentVertex.Position] = PreviousVertex;
            }
        }

        protected override void InspectVertex(IVertex vertex)
        {
            PreviousVertex = vertex;
        }
    }
}