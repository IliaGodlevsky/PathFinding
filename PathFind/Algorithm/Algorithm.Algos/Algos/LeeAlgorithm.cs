using Algorithm.Base;
using Algorithm.NullRealizations;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Algos.Algos
{
    public sealed class LeeAlgorithm : BreadthFirstAlgorithm<Queue<IVertex>>
    {
        public LeeAlgorithm(IEndPoints endPoints)
            : base(endPoints)
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
