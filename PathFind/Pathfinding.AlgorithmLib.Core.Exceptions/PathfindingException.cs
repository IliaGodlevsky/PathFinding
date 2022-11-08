using System;
using System.Runtime.Serialization;

namespace Pathfinding.AlgorithmLib.Core.Exceptions
{
    public class PathfindingException : Exception
    {
        public PathfindingException()
        {

        }

        public PathfindingException(string message) : base(message)
        {

        }

        public PathfindingException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected PathfindingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
