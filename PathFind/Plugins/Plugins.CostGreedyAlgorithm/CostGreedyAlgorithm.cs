using Algorithm.Base;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;
using GraphLib.Realizations.StepRules;

namespace Plugins.CostGreedyAlgorithm
{
    [ClassName("Cost greedy algorithm")]
    public sealed class CostGreedyAlgorithm : BaseGreedyAlgorithm
    {
        public CostGreedyAlgorithm(IGraph graph, IEndPoints endPoints) :
            this(graph, endPoints, new DefaultStepRule())
        {

        }

        public CostGreedyAlgorithm(IGraph graph,
            IEndPoints endPoints, IStepRule stepRule) :
            base(graph, endPoints)
        {
            this.stepRule = stepRule;
        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            var cost = stepRule.StepCost(vertex, CurrentVertex);
            return cost;
        }

        private readonly IStepRule stepRule;
    }
}
