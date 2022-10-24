using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Algos
{
    public sealed class DepthFirstAlgorithm : GreedyAlgorithm
    {
        private readonly IHeuristic heuristic;

        public DepthFirstAlgorithm(IPathfindingRange endPoints, IHeuristic heuristic)
            : base(endPoints)
        {
            this.heuristic = heuristic;
        }

        public DepthFirstAlgorithm(IPathfindingRange endPoints)
            : this(endPoints, new ManhattanDistance())
        {

        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, pathfindingRange.Source);
        }

        public override string ToString()
        {
            return "Depth first algorithm";
        }
    }
}