using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class WaveAlgorithm<TStorage> : PathfindingAlgorithm<TStorage>
        where TStorage : new()
    {
        protected WaveAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        protected abstract void RelaxVertex(IPathfindingVertex vertex);

        protected override void PrepareForSubPathfinding(
            (IPathfindingVertex Source, IPathfindingVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            VisitCurrentVertex();
        }

        protected override void VisitCurrentVertex()
        {
            visited.Add(CurrentVertex);
        }

        protected virtual void RelaxNeighbours(IReadOnlyCollection<IPathfindingVertex> vertices)
        {
            vertices.ForEach(RelaxVertex);
        }

        protected override void InspectCurrentVertex()
        {
            var neighbours = GetUnvisitedNeighbours(CurrentVertex);
            RaiseVertexProcessed(CurrentVertex, neighbours);
            RelaxNeighbours(neighbours);
        }
    }
}