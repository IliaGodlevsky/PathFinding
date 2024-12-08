using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class AStarGreedyAlgorithm : GreedyAlgorithm
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;

        public AStarGreedyAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange,
            IHeuristic heuristic, IStepRule stepRule) : base(pathfindingRange)
        {
            this.stepRule = stepRule;
            this.heuristic = heuristic;
        }

        public AStarGreedyAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : this(pathfindingRange, new ChebyshevDistance(), new DefaultStepRule())
        {

        }

        protected override IGraphPath GetSubPath()
        {
            return new GraphPath(traces.ToDictionary(),
                CurrentRange.Target, stepRule);
        }

        protected override double CalculateGreed(IPathfindingVertex vertex)
        {
            double heuristicResult = heuristic.Calculate(vertex, CurrentRange.Target);
            double stepCost = stepRule.CalculateStepCost(vertex, CurrentVertex);
            return heuristicResult + stepCost;
        }
    }
}