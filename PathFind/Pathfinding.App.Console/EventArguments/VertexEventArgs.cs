using Pathfinding.App.Console.Model;
using System;

namespace Pathfinding.App.Console.EventArguments
{
    internal class VertexEventArgs : EventArgs
    {
        public Vertex Current { get; }

        public VertexEventArgs(Vertex current)
        {
            Current = current;
        }
    }
}
