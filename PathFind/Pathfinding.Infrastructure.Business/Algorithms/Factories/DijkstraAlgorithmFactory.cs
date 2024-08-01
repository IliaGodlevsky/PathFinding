using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Factories
{
    public sealed class DijkstraAlgorithmFactory : IAlgorithmFactory<DijkstraAlgorithm>
    {
        private readonly IStepRule stepRule;

        public DijkstraAlgorithmFactory(IStepRule stepRule)
        {
            this.stepRule = stepRule;
        }

        public DijkstraAlgorithmFactory()
            : this(new DefaultStepRule())
        {

        }

        public DijkstraAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new DijkstraAlgorithm(pathfindingRange, stepRule);
        }
    }
}