using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    public sealed class HeuristicCostGreedyAlgorithm : GreedyAlgorithm
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;

        public HeuristicCostGreedyAlgorithm(IPathfindingRange pathfindingRange,
            IHeuristic heuristic, IStepRule stepRule) : base(pathfindingRange)
        {
            this.stepRule = stepRule;
            this.heuristic = heuristic;
        }

        public HeuristicCostGreedyAlgorithm(IPathfindingRange pathfindingRange)
            : this(pathfindingRange, new ChebyshevDistance(), new DefaultStepRule())
        {

        }

        protected override IGraphPath CreateGraphPath()
        {
            return new GraphPath(traces.ToReadOnly(), CurrentRange.Target, stepRule);
        }

        protected override double CalculateHeuristic(IVertex vertex)
        {
            double heuristicResult = heuristic.Calculate(vertex, endPoints.Target);
            double stepCost = stepRule.CalculateStepCost(vertex, CurrentVertex);
            return heuristicResult + stepCost;
        }

        public override string ToString()
        {
            return "Cost greedy (heuristic)";
        }
    }
}