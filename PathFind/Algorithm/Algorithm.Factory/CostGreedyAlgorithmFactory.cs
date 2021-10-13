using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
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

        public IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new CostGreedyAlgorithm(graph, endPoints, stepRule);
        }

        private readonly IStepRule stepRule;
    }
}
