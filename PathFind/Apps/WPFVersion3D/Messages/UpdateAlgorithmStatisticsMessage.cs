namespace WPFVersion3D.Messages
{
    internal readonly struct UpdateAlgorithmStatisticsMessage
    {
        public int Index { get; }
        public string Statistics { get; }

        public UpdateAlgorithmStatisticsMessage(int index, string statistics)
        {
            Index = index;
            Statistics = statistics;
        }
    }
}