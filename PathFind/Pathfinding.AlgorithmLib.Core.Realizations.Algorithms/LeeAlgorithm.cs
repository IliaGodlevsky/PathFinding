using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    public sealed class LeeAlgorithm : BreadthFirstAlgorithm<Queue<IVertex>>
    {
        public LeeAlgorithm(IPathfindingRange pathfindingRange)
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
            return storage.Count == 0
                ? DeadEndVertex.Interface
                : storage.Dequeue();
        }

        protected override void RelaxVertex(IVertex vertex)
        {
            storage.Enqueue(vertex);
            base.RelaxVertex(vertex);
        }

        public override string ToString()
        {
            return "Lee algorithm";
        }
    }
}
