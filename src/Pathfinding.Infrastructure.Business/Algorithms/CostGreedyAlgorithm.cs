﻿using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class CostGreedyAlgorithm : GreedyAlgorithm
    {
        private readonly IStepRule stepRule;

        public CostGreedyAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule())
        {

        }

        public CostGreedyAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange,
            IStepRule stepRule)
            : base(pathfindingRange)
        {
            this.stepRule = stepRule;
        }

        protected override IGraphPath GetSubPath()
        {
            return new GraphPath(traces.ToImmutableDictionary(),
                CurrentRange.Target, stepRule);
        }

        protected override double CalculateGreed(IPathfindingVertex vertex)
        {
            return stepRule.CalculateStepCost(vertex, CurrentVertex);
        }
    }
}