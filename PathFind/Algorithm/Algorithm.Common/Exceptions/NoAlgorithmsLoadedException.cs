using System;
using System.Runtime.Serialization;

namespace Algorithm.Common.Exceptions
{
    [Serializable]
    public class NoAlgorithmsLoadedException : Exception
    {
        public NoAlgorithmsLoadedException() : base()
        {

        }

        public NoAlgorithmsLoadedException(string message)
            : base(message)
        {

        }

        public NoAlgorithmsLoadedException(string message, Exception innnerException)
            : base(message, innnerException)
        {

        }

        public NoAlgorithmsLoadedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}