using Pathfinding.Domain.Interface;
using System;

namespace Pathfinding.Infrastructure.Business.Algorithms.Events
{
    public class PathfindingEventArgs : EventArgs
    {
        public ICoordinate Current { get; }

        public PathfindingEventArgs(IVertex current)
        {
            Current = current.Position;
        }
    }
}