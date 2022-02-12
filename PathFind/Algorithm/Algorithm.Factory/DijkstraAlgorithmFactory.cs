using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Common.Attrbiutes;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Order(1)]
    [WaveGroup]
    [Description("Dijkstra's algorithm")]

    public sealed class DijkstraAlgorithmFactory : IAlgorithmFactory
    {
        public DijkstraAlgorithmFactory(IStepRule stepRule)
        {
            this.stepRule = stepRule;
        }

        public DijkstraAlgorithmFactory() : this(new DefaultStepRule())
        {

        }

        public PathfindingAlgorithm Create(IEndPoints endPoints)
        {
            return new DijkstraAlgorithm(endPoints, stepRule);
        }

        private readonly IStepRule stepRule;
    }
}
