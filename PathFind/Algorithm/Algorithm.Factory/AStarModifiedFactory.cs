using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Description("A* modified algorithm")]
    public sealed class AStarModifiedFactory : IAlgorithmFactory
    {
        public AStarModifiedFactory(IStepRule stepRule, IHeuristic heuristic)
        {
            this.heuristic = heuristic;
            this.stepRule = stepRule;
        }

        public AStarModifiedFactory(IStepRule stepRule) : this(stepRule, new ChebyshevDistance())
        {

        }

        public AStarModifiedFactory(IHeuristic heuristic) : this(new DefaultStepRule(), heuristic)
        {

        }

        public AStarModifiedFactory() : this(new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new AStarModified(graph, endPoints, stepRule, heuristic);
        }

        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;
    }
}
