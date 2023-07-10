using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Attributes;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Factory
{
    [Order(3)]
    [WaveGroup]
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

        public override string ToString()
        {
            return Languages.IDAStarAlgorithm;
        }
    }
}