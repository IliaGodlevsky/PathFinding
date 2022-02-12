using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [GreedyGroup]
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

        public PathfindingAlgorithm Create(IEndPoints endPoints)
        {
            return new DepthFirstAlgorithm(endPoints, heuristic);
        }

        private readonly IHeuristic heuristic;
    }
}
