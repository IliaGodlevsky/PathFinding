using System;
using System.Runtime.Serialization;

namespace Algorithm.Common.Exceptions
{
    [Serializable]
    public class AlgorithmInterruptedException : Exception
    {
        public AlgorithmInterruptedException() : base()
        {

        }

        public AlgorithmInterruptedException(string message) 
            : base(message)
        {

        }

        public AlgorithmInterruptedException(string message, Exception innnerException)
            : base(message, innnerException)
        {

        }

        public AlgorithmInterruptedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}