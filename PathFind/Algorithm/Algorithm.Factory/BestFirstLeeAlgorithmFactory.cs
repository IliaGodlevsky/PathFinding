using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [WaveGroup(5)]
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

        public PathfindingAlgorithm CreateAlgorithm(IEndPoints endPoints)
        {
            return new BestFirstLeeAlgorithm(endPoints, heuristic);
        }

        private readonly IHeuristic heuristic;
    }
}
