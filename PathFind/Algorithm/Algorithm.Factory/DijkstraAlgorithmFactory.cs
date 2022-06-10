using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Common.Attrbiutes;
using GraphLib.Interfaces;

namespace Algorithm.Factory
{
    [Order(1)]
    [WaveGroup]
    public sealed class DijkstraAlgorithmFactory : IAlgorithmFactory<DijkstraAlgorithm>
    {
        private readonly IStepRule stepRule;

        public DijkstraAlgorithmFactory(IStepRule stepRule)
        {
            this.stepRule = stepRule;
        }

        public DijkstraAlgorithmFactory()
            : this(new DefaultStepRule())
        {

        }

        public DijkstraAlgorithm Create(IEndPoints endPoints)
        {
            return new DijkstraAlgorithm(endPoints, stepRule);
        }

        public override string ToString()
        {
            return "Dijkstra's algorithm";
        }
    }
}