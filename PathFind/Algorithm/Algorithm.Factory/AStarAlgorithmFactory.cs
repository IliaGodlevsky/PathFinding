using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using Common.Attrbiutes;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Order(2)]
    [WaveGroup]
    [Description("A* algorithm")]
    public sealed class AStarAlgorithmFactory : IAlgorithmFactory
    {
        public AStarAlgorithmFactory(IStepRule stepRule, IHeuristic heuristic)
        {
            this.heuristic = heuristic;
            this.stepRule = stepRule;
        }

        public AStarAlgorithmFactory(IStepRule stepRule) : this(stepRule, new ChebyshevDistance())
        {

        }

        public AStarAlgorithmFactory(IHeuristic heuristic) : this(new DefaultStepRule(), heuristic)
        {

        }

        public AStarAlgorithmFactory() : this(new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public PathfindingAlgorithm Create(IEndPoints endPoints)
        {
            return new AStarAlgorithm(endPoints, stepRule, heuristic);
        }

        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;
    }
}
