using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    public sealed class CostGreedyAlgorithm : GreedyAlgorithm
    {
        private readonly IStepRule stepRule;

        public CostGreedyAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule())
        {

        }

        public CostGreedyAlgorithm(IEnumerable<IVertex> pathfindingRange, IStepRule stepRule)
            : base(pathfindingRange)
        {
            this.stepRule = stepRule;
        }

        protected override IGraphPath GetSubPath()
        {
            var traces = this.traces.ToDictionary();
            return new GraphPath(traces, CurrentRange.Target, stepRule);
        }

        protected override double CalculateHeuristic(IVertex vertex)
        {
            return stepRule.CalculateStepCost(vertex, CurrentVertex);
        }

        public override string ToString()
        {
            return Languages.CostGreedyAlgorithm;
        }
    }
}