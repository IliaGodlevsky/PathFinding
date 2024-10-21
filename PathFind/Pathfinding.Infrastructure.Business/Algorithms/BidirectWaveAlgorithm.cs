using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class BidirectWaveAlgorithm<TStorage> : BidirectPathfindingAlgorithm<TStorage>
        where TStorage : new()
    {
        protected BidirectWaveAlgorithm(IEnumerable<IVertex> pathfindingRange) 
            : base(pathfindingRange)
        {
        }

        protected abstract void RelaxForwardVertex(IVertex vertex);

        protected abstract void RelaxBackwardVertex(IVertex vertex);

        protected override void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            VisitCurrentVertex();
        }

        protected override void VisitCurrentVertex()
        {
            forwardVisited.Add(Current.Forward);
            backwardVisited.Add(Current.Backward);
        }

        protected virtual void RelaxForwardNeighbours(IReadOnlyCollection<IVertex> vertices)
        {
            vertices.ForEach(RelaxForwardVertex);
        }

        protected virtual void RelaxBackwardNeighbours(IReadOnlyCollection<IVertex> vertices)
        {
            vertices.ForEach(RelaxBackwardVertex);
        }

        protected override void InspectCurrentVertex()
        {
            var forwardNeighbors = GetForwardUnvisitedNeighbours();
            var backwardNeighbors = GetBackwardUnvisitedNeighbours();
            RaiseVertexProcessed(Current.Forward, forwardNeighbors);
            RaiseVertexProcessed(Current.Backward, backwardNeighbors);
            RelaxForwardNeighbours(forwardNeighbors);
            RelaxBackwardNeighbours(backwardNeighbors);
        }
    }
}
