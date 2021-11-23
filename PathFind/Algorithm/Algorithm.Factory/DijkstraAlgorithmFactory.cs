using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [WaveGroup(1)]
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

        public PathfindingAlgorithm CreateAlgorithm(IEndPoints endPoints)
        {
            return new DijkstraAlgorithm(endPoints, stepRule);
        }

        private readonly IStepRule stepRule;
    }
}
