using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Algos
{
    public class DistanceFirstAlgorithm : GreedyAlgorithm
    {
        private readonly IHeuristic heuristic;

        public DistanceFirstAlgorithm(IPathfindingRange endPoints, IHeuristic heuristic)
            : base(endPoints)
        {
            this.heuristic = heuristic;
        }

        public DistanceFirstAlgorithm(IPathfindingRange endPoints)
            : this(endPoints, new EuclidianDistance())
        {

        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, pathfindingRange.Target);
        }

        public override string ToString()
        {
            return "Distance first algorithm";
        }
    }
}