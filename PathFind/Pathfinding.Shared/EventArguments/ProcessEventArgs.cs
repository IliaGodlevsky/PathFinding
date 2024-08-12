using System;

namespace Pathfinding.Shared.EventArguments
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
