using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [GreedyGroup]
    [Description("Depth first algorithm")]
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

        public DepthFirstAlgorithm Create(IEndPoints endPoints)
        {
            return new DepthFirstAlgorithm(endPoints, heuristic);
        }
    }
}