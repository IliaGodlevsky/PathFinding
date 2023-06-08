using System;
using System.Runtime.Serialization;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions
{
    public class SerializationException : SystemException
    {
        public SerializationException(string message)
            : base(message)
        {

        }

        public SerializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected SerializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}