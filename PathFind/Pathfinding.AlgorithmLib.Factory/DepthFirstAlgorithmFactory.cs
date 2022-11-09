using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Factory
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

        public DepthFirstAlgorithm Create(IPathfindingRange pathfindingRange)
        {
            return new DepthFirstAlgorithm(pathfindingRange, heuristic);
        }

        public override string ToString()
        {
            return "Depth first algorithm";
        }
    }
}