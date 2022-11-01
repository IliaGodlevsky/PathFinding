using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Algos
{
    public class DistanceFirstAlgorithm : GreedyAlgorithm
    {
        private readonly IHeuristic heuristic;

        public DistanceFirstAlgorithm(IEndPoints endPoints, IHeuristic heuristic)
            : base(endPoints)
        {
            this.heuristic = heuristic;
        }

        public DistanceFirstAlgorithm(IEndPoints endPoints)
            : this(endPoints, new EuclidianDistance())
        {

        }

        public override PathfindingAlgorithm GetClone()
        {
            return new DistanceFirstAlgorithm(endPoints, heuristic);
        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, endPoints.Target);
        }

        public override string ToString()
        {
            return "Distance first algorithm";
        }
    }
}