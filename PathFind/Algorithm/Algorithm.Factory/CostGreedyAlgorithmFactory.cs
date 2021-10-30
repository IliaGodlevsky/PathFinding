using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [GreedyGroup]
    [Description("Cost greedy algorithm")]
    public sealed class CostGreedyAlgorithmFactory : IAlgorithmFactory
    {
        public CostGreedyAlgorithmFactory(IStepRule stepRule)
        {
            this.stepRule = stepRule;
        }

        public CostGreedyAlgorithmFactory() : this(new DefaultStepRule())
        {

        }

        public PathfindingAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new CostGreedyAlgorithm(graph, endPoints, stepRule);
        }

        private readonly IStepRule stepRule;
    }
}
