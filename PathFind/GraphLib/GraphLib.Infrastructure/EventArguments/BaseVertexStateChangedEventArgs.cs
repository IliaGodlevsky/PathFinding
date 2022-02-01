using GraphLib.Interfaces;
using System;

namespace GraphLib.Infrastructure.EventArguments
{
    public abstract class BaseVertexChangedEventArgs<T> : EventArgs
    {
        public T Changed { get; }

        public IVertex Vertex { get; }

        public BaseVertexChangedEventArgs(T changed, IVertex vertex)
        {
            Changed = changed;
            Vertex = vertex;
        }
    }
}
