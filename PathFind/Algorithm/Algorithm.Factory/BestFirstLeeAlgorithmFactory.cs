using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Description("Lee algorithm (heuristic)")]
    public sealed class BestFirstLeeAlgorithmFactory : IAlgorithmFactory
    {
        public BestFirstLeeAlgorithmFactory(IHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public BestFirstLeeAlgorithmFactory() : this(new ManhattanDistance())
        {

        }

        public PathfindingAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new BestFirstLeeAlgorithm(graph, endPoints, heuristic);
        }

        private readonly IHeuristic heuristic;
    }
}
