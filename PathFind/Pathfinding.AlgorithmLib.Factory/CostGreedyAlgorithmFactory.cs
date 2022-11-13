﻿using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Factory
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

        public CostGreedyAlgorithm Create(IPathfindingRange pathfindingRange)
        {
            return new CostGreedyAlgorithm(pathfindingRange, stepRule);
        }

        public override string ToString()
        {
            return "Cost greedy algorithm";
        }
    }
}