using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class PathfindingRangeChosenMessage
    {
        public Guid Key { get; }

        public IPathfindingRange<Vertex> Range { get; }

        public PathfindingRangeChosenMessage(IPathfindingRange<Vertex> range, Guid key)
        {
            Key = key;
            Range = range;
        }
    }
}