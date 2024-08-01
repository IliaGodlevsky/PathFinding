using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Factories
{
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

        public HeuristicCostGreedyAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new HeuristicCostGreedyAlgorithm(pathfindingRange, heuristic, stepRule);
        }
    }
}