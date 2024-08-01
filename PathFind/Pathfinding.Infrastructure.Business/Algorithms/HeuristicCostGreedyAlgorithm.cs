using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class HeuristicCostGreedyAlgorithm : GreedyAlgorithm
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;

        public HeuristicCostGreedyAlgorithm(IEnumerable<IVertex> pathfindingRange,
            IHeuristic heuristic, IStepRule stepRule) : base(pathfindingRange)
        {
            this.stepRule = stepRule;
            this.heuristic = heuristic;
        }

        public HeuristicCostGreedyAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new ChebyshevDistance(), new DefaultStepRule())
        {

        }

        protected override IGraphPath GetSubPath()
        {
            return new GraphPath(traces.ToDictionary(),
                CurrentRange.Target, stepRule);
        }

        protected override double CalculateHeuristic(IVertex vertex)
        {
            double heuristicResult = heuristic.Calculate(vertex, CurrentRange.Target);
            double stepCost = stepRule.CalculateStepCost(vertex, CurrentVertex);
            return heuristicResult + stepCost;
        }
    }
}