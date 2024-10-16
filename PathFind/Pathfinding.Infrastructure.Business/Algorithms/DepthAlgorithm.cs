using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class DepthAlgorithm : PathfindingAlgorithm<Stack<IVertex>>
    {
        private IVertex PreviousVertex { get; set; } = NullVertex.Instance;

        protected DepthAlgorithm(IEnumerable<IVertex> pathfindingRange)
           : base(pathfindingRange)
        {
        }

        protected abstract IVertex GetVertex(IReadOnlyCollection<IVertex> neighbors);

        protected override IVertex GetNextVertex()
        {
            var neighbours = GetUnvisitedNeighbours(CurrentVertex);
            RaiseVertexProcessed(CurrentVertex, neighbours);
            return GetVertex(neighbours);
        }

        protected override void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            visited.Add(CurrentVertex);
            storage.Push(CurrentVertex);
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
                CurrentVertex = storage.PopOrThrowDeadEndVertexException();
            }
            else
            {
                visited.Add(CurrentVertex);
                storage.Push(CurrentVertex);
                traces[CurrentVertex.Position] = PreviousVertex;
            }
        }

        protected override void InspectCurrentVertex()
        {
            PreviousVertex = CurrentVertex;
        }
    }
}