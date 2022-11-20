using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    internal class DistanceFirstAlgorithm : GreedyAlgorithm
    {
        private readonly IHeuristic heuristic;

        public DistanceFirstAlgorithm(IPathfindingRange pathfindingRange, IHeuristic heuristic)
            : base(pathfindingRange)
        {
            this.heuristic = heuristic;
        }

        public DistanceFirstAlgorithm(IPathfindingRange pathfindingRange)
            : this(pathfindingRange, new EuclidianDistance())
        {

        }

        protected override double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Target);
        }

        public override string ToString()
        {
            return "Distance first algorithm";
        }
    }
}