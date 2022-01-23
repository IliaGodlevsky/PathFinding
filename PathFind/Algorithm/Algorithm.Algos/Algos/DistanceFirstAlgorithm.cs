using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using GraphLib.Interfaces;
using Interruptable.Interface;
using System;
using System.Diagnostics;

namespace Algorithm.Algos.Algos
{
    [DebuggerDisplay("Distance first algorithm")]
    public class DistanceFirstAlgorithm : GreedyAlgorithm,
        IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        public DistanceFirstAlgorithm(IEndPoints endPoints, IHeuristic heuristic)
            : base(endPoints)
        {
            this.heuristic = heuristic;
        }

        public DistanceFirstAlgorithm(IEndPoints endPoints)
            : this(endPoints, new EuclidianDistance())
        {

        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, endPoints.Target);
        }

        private readonly IHeuristic heuristic;
    }
}
