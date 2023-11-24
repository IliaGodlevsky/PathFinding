using System;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class AlgorithmDelayMessage
    {
        public TimeSpan Delay { get; }

        public AlgorithmDelayMessage(TimeSpan delay)
        {
            Delay = delay;
        }
    }
}
