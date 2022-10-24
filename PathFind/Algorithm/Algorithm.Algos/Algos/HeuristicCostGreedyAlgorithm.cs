using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Algos
{
    public sealed class HeuristicCostGreedyAlgorithm : GreedyAlgorithm
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;

        public HeuristicCostGreedyAlgorithm(IPathfindingRange endPoints,
            IHeuristic heuristic, IStepRule stepRule) : base(endPoints)
        {
            this.stepRule = stepRule;
            this.heuristic = heuristic;
        }

        public HeuristicCostGreedyAlgorithm(IPathfindingRange endPoints)
            : this(endPoints, new ChebyshevDistance(), new DefaultStepRule())
        {

        }

        protected override IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, pathfindingRange, stepRule);
        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            var heuristicResult = heuristic.Calculate(vertex, pathfindingRange.Target);
            var stepCost = stepRule.CalculateStepCost(vertex, CurrentVertex);
            return heuristicResult + stepCost;
        }

        public override string ToString()
        {
            return "Cost greedy (heuristic)";
        }
    }
}