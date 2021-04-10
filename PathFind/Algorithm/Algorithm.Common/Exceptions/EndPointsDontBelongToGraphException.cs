using System;
using System.Runtime.Serialization;

namespace Algorithm.Common.Exceptions
{
    [Serializable]
    public class EndPointsDontBelongToGraphException : Exception
    {
        public EndPointsDontBelongToGraphException() : base()
        {

        }

        public EndPointsDontBelongToGraphException(string message)
            : base(message)
        {

        }

        public EndPointsDontBelongToGraphException(string message, Exception innnerException)
            : base(message, innnerException)
        {

        }

        public EndPointsDontBelongToGraphException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}