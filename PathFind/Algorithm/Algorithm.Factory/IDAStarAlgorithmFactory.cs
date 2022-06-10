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
    [Order(3)]
    [WaveGroup]
    public sealed class IDAStarAlgorithmFactory : IAlgorithmFactory<IDAStarAlgorithm>
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;

        public IDAStarAlgorithmFactory(IStepRule stepRule, IHeuristic heuristic)
        {
            this.heuristic = heuristic;
            this.stepRule = stepRule;
        }

        public IDAStarAlgorithmFactory(IStepRule stepRule)
            : this(stepRule, new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithmFactory(IHeuristic heuristic)
            : this(new DefaultStepRule(), heuristic)
        {

        }

        public IDAStarAlgorithmFactory()
            : this(new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithm Create(IEndPoints endPoints)
        {
            return new IDAStarAlgorithm(endPoints, stepRule, heuristic);
        }

        public override string ToString()
        {
            return "IDA* algorithm";
        }
    }
}