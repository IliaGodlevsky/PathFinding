namespace WPFVersion3D.Messages
{
    internal sealed class AlgorithmStatisticsIndexMessage
    {
        public int Index { get; }

        public AlgorithmStatisticsIndexMessage(int index)
        {
            Index = index;
        }
    }
}
