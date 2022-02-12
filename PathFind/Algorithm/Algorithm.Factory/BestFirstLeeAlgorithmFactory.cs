using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Common.Attrbiutes;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Order(5)]
    [WaveGroup]
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

        public PathfindingAlgorithm Create(IEndPoints endPoints)
        {
            return new BestFirstLeeAlgorithm(endPoints, heuristic);
        }

        private readonly IHeuristic heuristic;
    }
}
