using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;

namespace Algorithm.Factory
{
    [GreedyGroup]
    public sealed class CostGreedyAlgorithmFactory : IAlgorithmFactory<CostGreedyAlgorithm>
    {
        private readonly IStepRule stepRule;

        public CostGreedyAlgorithmFactory(IStepRule stepRule)
        {
            this.stepRule = stepRule;
        }

        public CostGreedyAlgorithmFactory()
            : this(new DefaultStepRule())
        {

        }

        public CostGreedyAlgorithm Create(IPathfindingRange endPoints)
        {
            return new CostGreedyAlgorithm(endPoints, stepRule);
        }

        public override string ToString()
        {
            return "Cost greedy algorithm";
        }
    }
}