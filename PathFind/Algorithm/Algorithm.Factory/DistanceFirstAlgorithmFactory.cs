using Algorithm.Algos.Algos;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using GraphLib.Interfaces;

namespace Algorithm.Factory
{
    [GreedyGroup]
    public sealed class DistanceFirstAlgorithmFactory : IAlgorithmFactory<DistanceFirstAlgorithm>
    {
        private readonly IHeuristic heuristic;

        public DistanceFirstAlgorithmFactory(IHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public DistanceFirstAlgorithmFactory()
            : this(new EuclidianDistance())
        {

        }

        public DistanceFirstAlgorithm Create(IEndPoints endPoints)
        {
            return new DistanceFirstAlgorithm(endPoints, heuristic);
        }

        public override string ToString()
        {
            return "Distance first algorithm";
        }
    }
}