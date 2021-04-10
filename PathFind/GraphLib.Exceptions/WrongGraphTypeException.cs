using System;
using System.Runtime.Serialization;
using GraphLib.Interface;

namespace GraphLib.Exceptions
{
    [Serializable]
    public class WrongGraphTypeException : GraphException
    {
        public WrongGraphTypeException(string message, IGraph graph) 
            : base(message, graph)
        {
           
        }

        public WrongGraphTypeException(string message) 
            : base(message)
        {

        }

        public WrongGraphTypeException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public WrongGraphTypeException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {

        }
    }
}