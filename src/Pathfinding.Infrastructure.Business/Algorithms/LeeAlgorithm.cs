using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class LeeAlgorithm : BreadthFirstAlgorithm<Queue<IPathfindingVertex>>
    {
        public LeeAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        protected override void DropState()
        {
            base.DropState();
            storage.Clear();
        }

        protected override void MoveNextVertex()
        {
            CurrentVertex = storage.DequeueOrThrowDeadEndVertexException();
        }

        protected override void RelaxVertex(IPathfindingVertex vertex)
        {
            storage.Enqueue(vertex);
            base.RelaxVertex(vertex);
        }
    }
}
