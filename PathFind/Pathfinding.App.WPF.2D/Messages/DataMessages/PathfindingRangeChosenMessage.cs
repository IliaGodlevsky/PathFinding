using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class PathfindingRangeChosenMessage
    {
        public Guid Id { get; }

        public IPathfindingRange<Vertex> PathfindingRange { get; }

        public PathfindingRangeChosenMessage(Guid id, IPathfindingRange<Vertex> pathfindingRange)
        {
            Id = id;
            PathfindingRange = pathfindingRange;
        }
    }
}
