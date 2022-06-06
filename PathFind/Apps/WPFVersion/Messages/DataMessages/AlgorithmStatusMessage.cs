namespace WPFVersion.Messages.DataMessages
{
    internal sealed class AlgorithmStatusMessage
    {
        public string Status { get; }
        public int Index { get; }

        public AlgorithmStatusMessage(string status, int index)
        {
            Index = index;
            Status = status;
        }
    }
}
