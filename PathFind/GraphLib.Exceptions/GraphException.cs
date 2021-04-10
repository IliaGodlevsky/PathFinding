using System;
using System.Runtime.Serialization;
using GraphLib.Interface;

namespace GraphLib.Exceptions
{
    public class GraphException : Exception
    {
        public IGraph Graph { get; }

        public GraphException(string message, IGraph graph)
            : base(message)
        {
            Graph = graph;
        }

        public GraphException(string message) : base(message)
        {

        }

        public GraphException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public GraphException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}