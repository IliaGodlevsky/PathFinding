using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Common.Attrbiutes;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [Order(5)]
    [WaveGroup]
    [Description("Lee algorithm (heuristic)")]
    public sealed class BestFirstLeeAlgorithmFactory : IAlgorithmFactory<BestFirstLeeAlgorithm>
    {
        private readonly IHeuristic heuristic;

        public BestFirstLeeAlgorithmFactory(IHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public BestFirstLeeAlgorithmFactory()
            : this(new ManhattanDistance())
        {

        }

        public BestFirstLeeAlgorithm Create(IEndPoints endPoints)
        {
            return new BestFirstLeeAlgorithm(endPoints, heuristic);
        }
    }
}