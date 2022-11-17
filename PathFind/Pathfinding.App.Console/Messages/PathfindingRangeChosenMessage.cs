using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathfindingRangeChosenMessage : IHistoryMessage
    {
        public PathfindingProcess Algorithm { get; }

        public IPathfindingRange Range { get; }

        public PathfindingRangeChosenMessage(IPathfindingRange range, PathfindingProcess algorithm)
        {
            Algorithm = algorithm;
            Range = range;
        }
    }
}