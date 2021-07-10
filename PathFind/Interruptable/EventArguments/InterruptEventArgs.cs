using System;

namespace Interruptable.EventArguments
{
    public class InterruptEventArgs : EventArgs
    {
        public InterruptEventArgs()
        {
            When = DateTime.Now;
        }

        public DateTime When { get; }
    }
}
