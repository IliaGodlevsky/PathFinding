using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.AlgorithmLib.Factory
{
    [Order(4)]
    [WaveGroup]
    public sealed class LeeAlgorithmFactory : IAlgorithmFactory<PathfindingProcess>
    {
        public PathfindingProcess Create(IPathfindingRange pathfindingRange)
        {
            return new LeeAlgorithm(pathfindingRange);
        }

        public override string ToString()
        {
            return "Lee algorithm";
        }
    }
}
