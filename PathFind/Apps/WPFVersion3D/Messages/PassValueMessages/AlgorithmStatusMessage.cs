using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class AlgorithmStatusMessage
    {
        public static AlgorithmStatusMessage Paused(int index)
        {
            return new AlgorithmStatusMessage(AlgorithmViewModel.Paused, index);
        }

        public static AlgorithmStatusMessage Interrupted(int index)
        {
            return new AlgorithmStatusMessage(AlgorithmViewModel.Interrupted, index);
        }

        public static AlgorithmStatusMessage Started(int index)
        {
            return new AlgorithmStatusMessage(AlgorithmViewModel.Started, index);
        }

        public string Status { get; }

        public int Index { get; }

        public AlgorithmStatusMessage(string statuses, int index)
        {
            Status = statuses;
            Index = index;
        }
    }
}
