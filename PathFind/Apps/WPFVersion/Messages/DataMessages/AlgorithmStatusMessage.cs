using WPFVersion.Enums;

namespace WPFVersion.Messages.DataMessages
{
    internal sealed class AlgorithmStatusMessage
    {
        public AlgorithmStatus Status { get; }
        public int Index { get; }

        public AlgorithmStatusMessage(AlgorithmStatus status, int index)
        {
            Index = index;
            Status = status;
        }
    }
}
