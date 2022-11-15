using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class PathfindingRangeChosenMessage
    {
        public IAlgorithm<IGraphPath> Algorithm { get; }

        public IPathfindingRange PathfindingRange { get; }

        public PathfindingRangeChosenMessage(IAlgorithm<IGraphPath> algorithm, IPathfindingRange pathfindingRange)
        {
            Algorithm = algorithm;
            PathfindingRange = pathfindingRange;
        }
    }
}
