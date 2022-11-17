using System;

namespace Pathfinding.App.WPF._2D.Messages.DataMessages
{
    internal sealed class DelayTimeChangedMessage
    {
        public TimeSpan DelayTime { get; }

        public Guid Id { get; }

        public DelayTimeChangedMessage(TimeSpan delayTime, Guid id)
        {
            DelayTime = delayTime;
            Id = id;
        }
    }
}
