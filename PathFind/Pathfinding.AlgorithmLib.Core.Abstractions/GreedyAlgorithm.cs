using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Extensions;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.NullObjects;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.Abstractions
{
    internal abstract class GreedyAlgorithm : PathfindingAlgorithm<Stack<IVertex>>
    {
        private IVertex PreviousVertex { get; set; } = NullVertex.Instance;

        protected GreedyAlgorithm(IEnumerable<IVertex> pathfindingRange)
           : base(pathfindingRange)
        {
        }

        protected abstract double CalculateHeuristic(IVertex vertex);

        protected override IVertex GetNextVertex()
        {
            var neighbours = GetUnvisitedNeighbours(CurrentVertex);
            double leastVertexCost = neighbours.Any() ? neighbours.Min(CalculateHeuristic) : default;
            return neighbours
                .ForEach(Enqueued)
                .FirstOrNullVertex(vertex => CalculateHeuristic(vertex) == leastVertexCost);
        }

        protected override void PrepareForSubPathfinding(SubRange range)
        {
            base.PrepareForSubPathfinding(range);
            VisitVertex(CurrentVertex);
        }

        private void VisitVertex(IVertex vertex)
        {
            visited.Add(vertex);
            RaiseVertexVisited(new PathfindingEventArgs(vertex));
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