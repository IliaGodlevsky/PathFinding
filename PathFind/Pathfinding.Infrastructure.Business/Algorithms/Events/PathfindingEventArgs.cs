using Pathfinding.Domain.Interface;
using Pathfinding.Shared.Primitives;
using System;

namespace Pathfinding.Infrastructure.Business.Algorithms.Events
{
    public class PathfindingEventArgs : EventArgs
    {
        public Coordinate Current { get; }

        public PathfindingEventArgs(IVertex current)
        {
            Current = current.Position;
        }
    }
}