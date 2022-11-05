using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Common.Attrbiutes;
using GraphLib.Interfaces;

namespace Algorithm.Factory
{
    [Order(5)]
    [WaveGroup]
    public sealed class AStarLeeAlgorithmFactory : IAlgorithmFactory<AStarLeeAlgorithm>
    {
        private readonly IHeuristic heuristic;

        public AStarLeeAlgorithmFactory(IHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public AStarLeeAlgorithmFactory()
            : this(new ManhattanDistance())
        {

        }

        public AStarLeeAlgorithm Create(IEndPoints endPoints)
        {
            return new AStarLeeAlgorithm(endPoints, heuristic);
        }

        public override string ToString()
        {
            return "A* lee algorithm";
        }
    }
}