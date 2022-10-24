using GraphLib.Interfaces;
using System;

namespace GraphLib.Utility.EventArguments
{
    public class VertexEventArgs : EventArgs
    {
        public IVertex Vertex { get; }

        public VertexEventArgs(IVertex vertex)
        {
            Vertex = vertex;
        }
    }
}
