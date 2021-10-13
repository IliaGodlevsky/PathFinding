using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Description("Depth first algorithm")]
    public sealed class DepthFirstAlgorithmFactory : IAlgorithmFactory
    {
        public DepthFirstAlgorithmFactory(IHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public DepthFirstAlgorithmFactory() : this(new ManhattanDistance())
        {

        }

        public IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new DepthFirstAlgorithm(graph, endPoints, heuristic);
        }

        private readonly IHeuristic heuristic;
    }
}
