using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using GraphLib.Interfaces;

namespace Algorithm.Factory
{
    [GreedyGroup]
    public sealed class DepthFirstAlgorithmFactory : IAlgorithmFactory<DepthFirstAlgorithm>
    {
        private readonly IHeuristic heuristic;

        public DepthFirstAlgorithmFactory(IHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public DepthFirstAlgorithmFactory()
            : this(new ManhattanDistance())
        {

        }

        public DepthFirstAlgorithm Create(IPathfindingRange endPoints)
        {
            return new DepthFirstAlgorithm(endPoints, heuristic);
        }

        public override string ToString()
        {
            return "Depth first algorithm";
        }
    }
}