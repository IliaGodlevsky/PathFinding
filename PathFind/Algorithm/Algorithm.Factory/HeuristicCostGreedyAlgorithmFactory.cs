using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [GreedyGroup]
    [Description("Cost greedy (heuristic)")]
    public sealed class HeuristicCostGreedyAlgorithmFactory : IAlgorithmFactory<HeuristicCostGreedyAlgorithm>
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;

        public HeuristicCostGreedyAlgorithmFactory(IStepRule stepRule, IHeuristic heuristic)
        {
            this.heuristic = heuristic;
            this.stepRule = stepRule;
        }

        public HeuristicCostGreedyAlgorithmFactory(IStepRule stepRule) 
            : this(stepRule, new ChebyshevDistance())
        {

        }

        public HeuristicCostGreedyAlgorithmFactory(IHeuristic heuristic) 
            : this(new DefaultStepRule(), heuristic)
        {

        }

        public HeuristicCostGreedyAlgorithmFactory() 
            : this(new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public HeuristicCostGreedyAlgorithm Create(IEndPoints endPoints)
        {
            return new HeuristicCostGreedyAlgorithm(endPoints, heuristic, stepRule);
        }
    }
}