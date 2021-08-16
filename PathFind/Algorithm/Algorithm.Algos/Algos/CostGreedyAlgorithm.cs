using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Algos
{
    public sealed class CostGreedyAlgorithm : GreedyAlgorithm
    {
        public CostGreedyAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
            : this(graph, endPoints, new DefaultStepRule())
        {

        }

        public CostGreedyAlgorithm(IGraph graph, IIntermediateEndPoints endPoints, IStepRule stepRule)
            : base(graph, endPoints)
        {
            this.stepRule = stepRule;
        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return stepRule.CalculateStepCost(vertex, CurrentVertex);
        }

        private readonly IStepRule stepRule;
    }
}
