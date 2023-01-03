using System;
using System.Runtime.Serialization;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions
{
    public class GraphSerializationException : SystemException
    {
        public GraphSerializationException(string message)
            : base(message)
        {

        }

        public GraphSerializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected GraphSerializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}