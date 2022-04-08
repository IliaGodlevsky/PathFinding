using WPFVersion3D.Enums;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class AlgorithmStatusMessage
    {
        public static AlgorithmStatusMessage Paused(int index) => new AlgorithmStatusMessage(AlgorithmStatuses.Paused, index);

        public static AlgorithmStatusMessage Interrupted(int index) => new AlgorithmStatusMessage(AlgorithmStatuses.Interrupted, index);

        public static AlgorithmStatusMessage Started(int index) => new AlgorithmStatusMessage(AlgorithmStatuses.Started, index);

        public AlgorithmStatuses Status { get; }

        public int Index { get; }

        public AlgorithmStatusMessage(AlgorithmStatuses statuses, int index)
        {
            Status = statuses;
            Index = index;
        }
    }
}
