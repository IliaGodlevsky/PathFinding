using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    internal sealed class CostGreedyAlgorithm : GreedyAlgorithm
    {
        private readonly IStepRule stepRule;

        public CostGreedyAlgorithm(IPathfindingRange pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule())
        {

        }
        public CostGreedyAlgorithm(IPathfindingRange pathfindingRange, IStepRule stepRule)
            : base(pathfindingRange)
        {
            this.stepRule = stepRule;
        }

        protected override IGraphPath GetSubPath()
        {
            var traces = new Dictionary<ICoordinate, IVertex>(this.traces);
            return new GraphPath(traces, CurrentRange.Target, stepRule);
        }

        protected override double CalculateHeuristic(IVertex vertex)
        {
            return stepRule.CalculateStepCost(vertex, CurrentVertex);
        }

        public override string ToString()
        {
            return "Cost greedy algorithm";
        }
    }
}