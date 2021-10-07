namespace WPFVersion3D.Messages
{
    internal sealed class AlgorithmsFinishedStatusMessage
    {
        public bool IsAllAlgorithmsFinished { get; }

        public AlgorithmsFinishedStatusMessage(bool isAllFinished)
        {
            IsAllAlgorithmsFinished = isAllFinished;
        }
    }
}
