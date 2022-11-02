using System;

namespace Shared.Process.EventArguments
{
    public class ProcessEventArgs : EventArgs
    {
        public DateTime When { get; }

        public ProcessEventArgs()
        {
            When = DateTime.UtcNow;
        }
    }
}
