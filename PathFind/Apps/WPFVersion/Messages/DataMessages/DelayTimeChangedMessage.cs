using System;

namespace WPFVersion.Messages.DataMessages
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
