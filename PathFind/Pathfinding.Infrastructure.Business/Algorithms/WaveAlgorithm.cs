using Pathfinding.Domain.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class WaveAlgorithm<TStorage> : PathfindingAlgorithm<TStorage>
        where TStorage : new()
    {
        protected WaveAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        protected abstract void RelaxVertex(IVertex vertex);

        protected override void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            VisitCurrentVertex();
        }

        protected override void VisitCurrentVertex()
        {
            visited.Add(CurrentVertex);
            RaiseVertexVisited(CurrentVertex);
        }

        protected virtual void RelaxNeighbours(IReadOnlyCollection<IVertex> vertices)
        {
            vertices.ForEach(RelaxVertex);
        }

        protected override void InspectVertex(IVertex vertex)
        {
            var neighbours = GetUnvisitedNeighbours(vertex);
            Enqueued(vertex, neighbours);
            RelaxNeighbours(neighbours);
        }
    }
}