namespace WPFVersion.Messages.DataMessages
{
    internal sealed class DelayTimeChangedMessage
    {
        public int DelayTime { get; }
        public int Index { get; }

        public DelayTimeChangedMessage(int delayTime, int index)
        {
            DelayTime = delayTime;
            Index = index;
        }
    }
}
