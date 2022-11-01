using System;
using System.Runtime.Serialization;

namespace Algorithm.Exceptions
{
    public class AlgorithmInterruptedException : Exception
    {
        public AlgorithmInterruptedException(object algorithm)
            : base($"{algorithm.ToString()} was interrupted")
        {

        }

        public AlgorithmInterruptedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected AlgorithmInterruptedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {

        }
    }
}
