using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;

namespace Plugins.CostGreedyAlgorithm
{
    [ClassName("Cost greedy algorithm")]
    public sealed class CostGreedyAlgorithm : BaseGreedyAlgorithm
    {
        public CostGreedyAlgorithm(IGraph graph, IEndPoints endPoints) 
            : this(graph, endPoints, new DefaultStepRule())
        {

        }

        public CostGreedyAlgorithm(IGraph graph, IEndPoints endPoints, IStepRule stepRule) 
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
