using WPFVersion3D.Enums;

namespace WPFVersion3D.Messages
{
    internal sealed class AlgorithmStatusMessage
    {
        public AlgorithmStatuses Status { get; }

        public int Index { get; }

        public AlgorithmStatusMessage(AlgorithmStatuses statuses, int index)
        {
            Status = statuses;
            Index = index;
        }
    }
}
