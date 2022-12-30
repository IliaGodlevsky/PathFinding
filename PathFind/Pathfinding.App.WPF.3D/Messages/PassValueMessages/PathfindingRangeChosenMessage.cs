using Pathfinding.App.WPF._3D.Model;
using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class PathfindingRangeChosenMessage : PassValueMessage<IPathfindingRange<Vertex3D>>
    {
        public Guid Id { get; }

        public PathfindingRangeChosenMessage(IPathfindingRange<Vertex3D> range, Guid id) : base(range)
        {
            Id = id;
        }
    }
}
