using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class PathfindingRangeChosenMessage : PassValueMessage<IPathfindingRange>
    {
        public Guid Id { get; }

        public PathfindingRangeChosenMessage(IPathfindingRange range, Guid id) : base(range)
        {
            Id = id;
        }
    }
}
