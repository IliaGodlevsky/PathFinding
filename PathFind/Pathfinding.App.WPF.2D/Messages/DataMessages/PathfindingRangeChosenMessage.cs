using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class PathfindingRangeChosenMessage
    {
        public Guid Id { get; }

        public IPathfindingRange PathfindingRange { get; }

        public PathfindingRangeChosenMessage(Guid id, IPathfindingRange pathfindingRange)
        {
            Id = id;
            PathfindingRange = pathfindingRange;
        }
    }
}
