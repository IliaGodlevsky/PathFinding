using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
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

        public IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new DijkstraAlgorithm(graph, endPoints, stepRule);
        }

        private readonly IStepRule stepRule;
    }
}
