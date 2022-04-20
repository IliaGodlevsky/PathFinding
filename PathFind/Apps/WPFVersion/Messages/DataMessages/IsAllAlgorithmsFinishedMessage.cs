namespace WPFVersion.Messages.DataMessages
{
    internal sealed class IsAllAlgorithmsFinishedMessage
    {
        public bool IsAllAlgorithmsFinished { get; }

        public IsAllAlgorithmsFinishedMessage(bool isAllFinished)
        {
            IsAllAlgorithmsFinished = isAllFinished;
        }
    }
}
