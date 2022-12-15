using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    internal sealed class HeuristicCostGreedyAlgorithm : GreedyAlgorithm
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
            var traces = new Dictionary<ICoordinate, IVertex>(this.traces);
            return new GraphPath(traces, CurrentRange.Target, stepRule);
        }

        protected override double CalculateHeuristic(IVertex vertex)
        {
            double heuristicResult = heuristic.Calculate(vertex, CurrentRange.Target);
            double stepCost = stepRule.CalculateStepCost(vertex, CurrentVertex);
            return heuristicResult + stepCost;
        }

        public override string ToString()
        {
            return Languages.HeuristicCostGreedyAlgorithm;
        }
    }
}