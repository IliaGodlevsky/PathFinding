using GraphLib.Interfaces;
using System;
using System.Runtime.Serialization;

namespace GraphLib.Exceptions
{
    public class LonelyVertexException : ArgumentException
    {
        public LonelyVertexException(IVertex vertex) : base(CreateMessage(vertex))
        {

        }

        public LonelyVertexException(IVertex vertex, Exception innerException)
            : base(CreateMessage(vertex), innerException)
        {

        }

        protected LonelyVertexException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private static string CreateMessage(IVertex vertex)
        {
            string cost = vertex.Cost.CurrentCost.ToString();
            string position = vertex.Position.ToString();
            string format = "Vertex doesn't belong to any graph. Vertex: {0}";
            return string.Format(format, $"[Cost: {cost}; position: {position}]");
        }
    }
}