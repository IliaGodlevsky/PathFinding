using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using GraphLib.Interfaces;
using Interruptable.Interface;
using System;

namespace Algorithm.Algos.Algos
{
    public sealed class DepthFirstAlgorithm : GreedyAlgorithm,
        IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        public DepthFirstAlgorithm(IGraph graph, IEndPoints endPoints, IHeuristic heuristic)
            : base(graph, endPoints)
        {
            this.heuristic = heuristic;
        }

        public DepthFirstAlgorithm(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, new ManhattanDistance())
        {

        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, endPoints.Source);
        }

        private readonly IHeuristic heuristic;
    }
}
