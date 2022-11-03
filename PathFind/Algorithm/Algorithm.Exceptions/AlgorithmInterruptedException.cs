using System;
using System.Runtime.Serialization;

namespace Algorithm.Exceptions
{
    public class AlgorithmInterruptedException : PathfindingException
    {
        public AlgorithmInterruptedException(object algorithm)
            : base($"{algorithm.ToString()} was interrupted")
        {

        }

        public AlgorithmInterruptedException(string message) : base(message)
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
