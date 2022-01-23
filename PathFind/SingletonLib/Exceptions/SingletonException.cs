using System;
using System.Runtime.Serialization;

namespace SingletonLib.Exceptions
{
    [Serializable]
    public class SingletonException : Exception
    {
        public SingletonException(string message) : base(message)
        {

        }

        public SingletonException(string message, Exception ex) : base(message, ex)
        {

        }

        protected SingletonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
