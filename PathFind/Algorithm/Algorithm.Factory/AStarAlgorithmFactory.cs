using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using Common.Attrbiutes;
using GraphLib.Interfaces;

namespace Algorithm.Factory
{
    [Order(2)]
    [WaveGroup]
    public sealed class AStarAlgorithmFactory : IAlgorithmFactory<AStarAlgorithm>
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;

        public AStarAlgorithmFactory(IStepRule stepRule, IHeuristic heuristic)
        {
            this.heuristic = heuristic;
            this.stepRule = stepRule;
        }

        public AStarAlgorithmFactory(IStepRule stepRule)
            : this(stepRule, new ChebyshevDistance())
        {

        }

        public AStarAlgorithmFactory(IHeuristic heuristic)
            : this(new DefaultStepRule(), heuristic)
        {

        }

        public AStarAlgorithmFactory()
            : this(new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public AStarAlgorithm Create(IEndPoints endPoints)
        {
            return new AStarAlgorithm(endPoints, stepRule, heuristic);
        }

        public override string ToString()
        {
            return "A* algorithm";
        }
    }
}