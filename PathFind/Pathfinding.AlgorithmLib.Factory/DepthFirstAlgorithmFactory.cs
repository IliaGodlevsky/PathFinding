using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Factory
{
    [GreedyGroup]
    public sealed class DepthFirstAlgorithmFactory : IAlgorithmFactory<PathfindingProcess>
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

        public PathfindingProcess Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new DepthFirstAlgorithm(pathfindingRange, heuristic);
        }

        public override string ToString()
        {
            return "Depth first algorithm";
        }
    }
}