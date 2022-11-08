using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    public sealed class CostGreedyAlgorithm : GreedyAlgorithm
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

        protected override IGraphPath CreateGraphPath()
        {
            return new GraphPath(traces.ToReadOnly(), CurrentRange.Target, stepRule);
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