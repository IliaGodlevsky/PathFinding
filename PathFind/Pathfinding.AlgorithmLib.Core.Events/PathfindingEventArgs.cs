using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.AlgorithmLib.Core.Events
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