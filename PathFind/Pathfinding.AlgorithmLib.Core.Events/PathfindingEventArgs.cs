using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.AlgorithmLib.Core.Events
{
    public class PathfindingEventArgs : EventArgs
    {
        public ICoordinate Current => Vertex.Position;

        public IVertex Vertex { get; }

        public PathfindingEventArgs(IVertex current)
        {
            Vertex = current;
        }
    }
}