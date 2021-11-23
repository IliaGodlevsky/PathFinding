using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using Interruptable.Interface;
using System;

namespace Algorithm.Algos.Algos
{
    /// <summary>
    /// An algorithm, that choses the cheapest neighbour around 
    /// a vertex and goes to it
    /// </summary>
    public sealed class CostGreedyAlgorithm : GreedyAlgorithm,
        IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CostGreedyAlgorithm"/>
        /// </summary>
        /// <param name="graph">A graph, where the cheapest path must be found</param>
        /// <param name="endPoints">Vertices, between which the cheapest path
        /// must be found</param>
        public CostGreedyAlgorithm(IEndPoints endPoints)
            : this(endPoints, new DefaultStepRule())
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="CostGreedyAlgorithm"/>
        /// </summary>
        /// <param name="graph">A graph, where the cheapest path must be found</param>
        /// <param name="endPoints">Vertices, between which the cheapest path
        /// must be found</param>
        /// <param name="stepRule">A way of calculating a step cost between vertices</param>
        public CostGreedyAlgorithm(IEndPoints endPoints, IStepRule stepRule)
            : base(endPoints)
        {
            this.stepRule = stepRule;
        }

        protected override IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, endPoints, stepRule);
        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return stepRule.CalculateStepCost(vertex, CurrentVertex);
        }

        private readonly IStepRule stepRule;
    }
}
