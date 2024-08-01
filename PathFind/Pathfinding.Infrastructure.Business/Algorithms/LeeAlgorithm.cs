using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Extensions;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class LeeAlgorithm : BreadthFirstAlgorithm<Queue<IVertex>>
    {
        public LeeAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        protected override void DropState()
        {
            base.DropState();
            storage.Clear();
        }

        protected override IVertex GetNextVertex()
        {
            return storage.DequeueOrThrowDeadEndVertexException();
        }

        protected override void RelaxVertex(IVertex vertex)
        {
            storage.Enqueue(vertex);
            base.RelaxVertex(vertex);
        }
    }
}
