using System;

namespace Common.EventArguments
{
    public class PathNotFoundEventArgs : EventArgs
    {
        public PathNotFoundEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
