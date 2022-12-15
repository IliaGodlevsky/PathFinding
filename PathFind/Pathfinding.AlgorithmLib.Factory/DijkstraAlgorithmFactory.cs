using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Attributes;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Factory
{
    [Order(1)]
    [WaveGroup]
    public sealed class DijkstraAlgorithmFactory : IAlgorithmFactory<PathfindingProcess>
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

        public PathfindingProcess Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new DijkstraAlgorithm(pathfindingRange, stepRule);
        }

        public override string ToString()
        {
            return Languages.DijkstraAlgorithm;
        }
    }
}