using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Factory
{
    [GreedyGroup]
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

        public HeuristicCostGreedyAlgorithm Create(IPathfindingRange pathfindingRange)
        {
            return new HeuristicCostGreedyAlgorithm(pathfindingRange, heuristic, stepRule);
        }

        public override string ToString()
        {
            return "Cost greedy (heuristic)";
        }
    }
}