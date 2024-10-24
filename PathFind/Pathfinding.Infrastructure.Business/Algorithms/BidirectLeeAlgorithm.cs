using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class BidirectLeeAlgorithm : BidirectBreadthFirstAlgorithm<Queue<IVertex>>
    {
        public BidirectLeeAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        protected override void DropState()
        {
            base.DropState();
            backwardStorage.Clear();
            forwardStorage.Clear();
        }

        protected override void RelaxForwardVertex(IVertex vertex)
        {
            forwardStorage.Enqueue(vertex);
            if (Intersection == NullVertex.Instance
                && backwardVisited.Contains(vertex))
            {
                Intersection = vertex;
            }
            base.RelaxForwardVertex(vertex);
        }

        protected override void RelaxBackwardVertex(IVertex vertex)
        {
            backwardStorage.Enqueue(vertex);
            if (Intersection == NullVertex.Instance
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