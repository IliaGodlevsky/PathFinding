using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathfindingRangeChosenMessage : IHistoryMessage
    {
        public PathfindingProcess Algorithm { get; }

        public IPathfindingRange<Vertex> Range { get; }

        public PathfindingRangeChosenMessage(IPathfindingRange<Vertex> range, PathfindingProcess algorithm)
        {
            Algorithm = algorithm;
            Range = range;
        }
    }
}