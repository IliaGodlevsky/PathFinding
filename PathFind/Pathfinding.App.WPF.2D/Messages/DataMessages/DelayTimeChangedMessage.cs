using System;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class DelayTimeChangedMessage
    {
        public TimeSpan DelayTime { get; }

        public int Index { get; }

        public DelayTimeChangedMessage(TimeSpan delayTime, int index)
        {
            DelayTime = delayTime;
            Index = index;
        }
    }
}
