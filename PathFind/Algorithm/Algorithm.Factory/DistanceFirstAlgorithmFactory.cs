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
    [Description("Distance first algorithm")]
    public sealed class DistanceFirstAlgorithmFactory : IAlgorithmFactory
    {
        public DistanceFirstAlgorithmFactory(IHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public DistanceFirstAlgorithmFactory() : this(new EuclidianDistance())
        {

        }

        public PathfindingAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new DistanceFirstAlgorithm(graph, endPoints, heuristic);
        }

        private readonly IHeuristic heuristic;
    }
}
