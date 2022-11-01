using System;

namespace Algorithm.Exceptions
{
    public class DeadendVertexException : Exception
    {
        private const string Msg = "Can't reach the destination";

        public DeadendVertexException() : base(Msg)
        {

        }

        public DeadendVertexException(string message) : base(message)
        {

        }
    }
}
