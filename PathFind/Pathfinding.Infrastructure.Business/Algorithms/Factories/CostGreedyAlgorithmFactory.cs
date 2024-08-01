using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Factories
{
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

        public CostGreedyAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new CostGreedyAlgorithm(pathfindingRange, stepRule);
        }
    }
}