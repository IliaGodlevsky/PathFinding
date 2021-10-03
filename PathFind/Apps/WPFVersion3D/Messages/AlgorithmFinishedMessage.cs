namespace WPFVersion3D.Messages
{
    internal sealed class AlgorithmFinishedMessage
    {
        public AlgorithmFinishedMessage(int index)
        {
            Index = index;
        }

        public int Index { get; }
    }
}
