using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Factories
{
    public sealed class IDAStarAlgorithmFactory : IAlgorithmFactory<IDAStarAlgorithm>
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;
        private readonly int stashPercent;

        public IDAStarAlgorithmFactory(IStepRule stepRule, IHeuristic heuristic, int stashPercent = 4)
        {
            this.heuristic = heuristic;
            this.stepRule = stepRule;
            this.stashPercent = stashPercent;
        }

        public IDAStarAlgorithmFactory(IStepRule stepRule)
            : this(stepRule, new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithmFactory(IHeuristic heuristic)
            : this(new DefaultStepRule(), heuristic)
        {

        }

        public IDAStarAlgorithmFactory()
            : this(new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new IDAStarAlgorithm(pathfindingRange, stepRule, heuristic, stashPercent);
        }
    }
}