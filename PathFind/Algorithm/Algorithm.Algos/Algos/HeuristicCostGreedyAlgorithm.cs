﻿using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Algos
{
    public sealed class HeuristicCostGreedyAlgorithm : GreedyAlgorithm
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;

        public HeuristicCostGreedyAlgorithm(IEndPoints endPoints,
            IHeuristic heuristic, IStepRule stepRule) : base(endPoints)
        {
            this.stepRule = stepRule;
            this.heuristic = heuristic;
        }

        public HeuristicCostGreedyAlgorithm(IEndPoints endPoints)
            : this(endPoints, new ChebyshevDistance(), new DefaultStepRule())
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