using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.Heuristic;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using System.ComponentModel;
using System.Diagnostics;

namespace Algorithm.Algos.Algos
{
    [DebuggerDisplay("Cost greedy (heuristic)")]
    [Description("Cost greedy (heuristic)")]
    public sealed class HeuristicCostGreedyAlgorithm : GreedyAlgorithm
    {
        public HeuristicCostGreedyAlgorithm(IEndPoints endPoints,
            IHeuristic heuristic, IStepRule stepRule) : base(endPoints)
        {
            this.stepRule = stepRule;
            this.heuristic = heuristic;
        }

        public HeuristicCostGreedyAlgorithm(IEndPoints endPoints)
            : this(endPoints, new ChebyshevDistance(), new DefaultStepRule())
        {

        }

        protected override IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, endPoints, stepRule);
        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            var heuristicResult = heuristic.Calculate(vertex, endPoints.Target);
            var stepCost = stepRule.CalculateStepCost(vertex, CurrentVertex);
            return heuristicResult + stepCost;
        }

        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;
    }
}
