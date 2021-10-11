namespace WPFVersion.Messages
{
    internal sealed class AlgorithmIndexMessage
    {
        public int Index { get; }

        public AlgorithmIndexMessage(int index)
        {
            Index = index;
        }
    }
}
