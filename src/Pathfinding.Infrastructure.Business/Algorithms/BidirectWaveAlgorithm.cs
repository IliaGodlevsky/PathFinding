﻿using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class BidirectWaveAlgorithm<TStorage>(IEnumerable<IPathfindingVertex> pathfindingRange) 
        : BidirectPathfindingAlgorithm<TStorage>(pathfindingRange)
        where TStorage : new()
    {
        protected abstract void RelaxForwardVertex(IPathfindingVertex vertex);

        protected abstract void RelaxBackwardVertex(IPathfindingVertex vertex);

        protected override void PrepareForSubPathfinding(
            (IPathfindingVertex Source, IPathfindingVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            VisitCurrentVertex();
        }

        protected override void VisitCurrentVertex()
        {
            forwardVisited.Add(Current.Forward);
            backwardVisited.Add(Current.Backward);
        }

        protected virtual void RelaxForwardNeighbours(IReadOnlyCollection<IPathfindingVertex> vertices)
        {
            vertices.ForEach(RelaxForwardVertex);
        }

        protected virtual void RelaxBackwardNeighbours(IReadOnlyCollection<IPathfindingVertex> vertices)
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
