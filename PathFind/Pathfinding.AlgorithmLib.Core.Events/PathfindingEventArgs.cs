using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.AlgorithmLib.Core.Events
{
    public class PathfindingEventArgs : EventArgs
    {
        public IVertex Current { get; }

        public PathfindingEventArgs(IVertex current)
        {
            Current = current;
        }
    }
}