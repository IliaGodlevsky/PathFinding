using System;
using System.Runtime.Serialization;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Exceptions
{
    public class CantSerializeGraphException : SystemException
    {
        public CantSerializeGraphException(string message)
            : base(message)
        {

        }

        public CantSerializeGraphException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected CantSerializeGraphException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}