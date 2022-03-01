using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using System.ComponentModel;
using System.Diagnostics;

namespace Algorithm.Algos.Algos
{
    [DebuggerDisplay("Cost greedy algorithm")]
    [Description("Cost greedy algorithm")]
    public sealed class CostGreedyAlgorithm : GreedyAlgorithm
    {
        public CostGreedyAlgorithm(IEndPoints endPoints)
            : this(endPoints, new DefaultStepRule())
        {

        }
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
