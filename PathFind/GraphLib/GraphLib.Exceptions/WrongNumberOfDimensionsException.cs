using System;
using System.Runtime.Serialization;

namespace GraphLib.Exceptions
{
    [Serializable]
    public class WrongNumberOfDimensionsException : ArgumentOutOfRangeException
    {
        public WrongNumberOfDimensionsException() : base()
        {

        }

        public WrongNumberOfDimensionsException(string paramName) : base(paramName)
        {

        }

        public WrongNumberOfDimensionsException(string paramName, string message)
            : base(paramName, message)
        {

        }

        public WrongNumberOfDimensionsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public WrongNumberOfDimensionsException(string paramName, object actualValue, string message)
            : base(paramName, actualValue, message)
        {

        }

        protected WrongNumberOfDimensionsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}