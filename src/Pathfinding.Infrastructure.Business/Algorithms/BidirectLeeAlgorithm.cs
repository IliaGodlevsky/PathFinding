﻿using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class BidirectLeeAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange) 
        : BidirectBreadthFirstAlgorithm<Queue<IPathfindingVertex>>(pathfindingRange)
    {
        protected override void DropState()
        {
            base.DropState();
            backwardStorage.Clear();
            forwardStorage.Clear();
        }

        protected override void RelaxForwardVertex(IPathfindingVertex vertex)
        {
            forwardStorage.Enqueue(vertex);
            if (Intersection == NullPathfindingVertex.Instance
                && backwardVisited.Contains(vertex))
            {
                Intersection = vertex;
            }
            base.RelaxForwardVertex(vertex);
        }

        protected override void RelaxBackwardVertex(IPathfindingVertex vertex)
        {
            backwardStorage.Enqueue(vertex);
            if (Intersection == NullPathfindingVertex.Instance
                && forwardVisited.Contains(vertex))
            {
                Intersection = vertex;
            }
            base.RelaxBackwardVertex(vertex);
        }

        protected override void MoveNextVertex()
        {
            var forward = forwardStorage.DequeueOrThrowDeadEndVertexException();
            var backward = backwardStorage.DequeueOrThrowDeadEndVertexException();
            Current = (forward, backward);
        }

        protected override void VisitCurrentVertex()
        {
            forwardVisited.Add(Current.Forward);
            backwardVisited.Add(Current.Backward);
        }
    }
}