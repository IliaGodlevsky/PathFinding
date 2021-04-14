using GraphLib.Interface;
using System;
using System.Runtime.Serialization;

namespace GraphLib.Exceptions
{
    public class NoVerticesToChooseAsEndPointsException : GraphException
    {

        public NoVerticesToChooseAsEndPointsException(string message, IGraph graph)
            : base(message, graph)
        {

        }

        public NoVerticesToChooseAsEndPointsException(string message) : base(message)
        {

        }

        public NoVerticesToChooseAsEndPointsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public NoVerticesToChooseAsEndPointsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}