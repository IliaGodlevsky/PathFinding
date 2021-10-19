using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Description("A* modified algorithm")]
    public sealed class AStarModifiedAlgorithmFactory : IAlgorithmFactory
    {
        public AStarModifiedAlgorithmFactory(IStepRule stepRule, IHeuristic heuristic)
        {
            this.heuristic = heuristic;
            this.stepRule = stepRule;
        }

        public AStarModifiedAlgorithmFactory(IStepRule stepRule) : this(stepRule, new ChebyshevDistance())
        {

        }

        public AStarModifiedAlgorithmFactory(IHeuristic heuristic) : this(new DefaultStepRule(), heuristic)
        {

        }

        public AStarModifiedAlgorithmFactory() : this(new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public PathfindingAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new AStarModified(graph, endPoints, stepRule, heuristic);
        }

        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;
    }
}
