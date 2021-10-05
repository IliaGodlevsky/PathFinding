using WPFVersion.Enums;

namespace WPFVersion.Messages
{
    internal sealed class AlgorithmsFinishedStatusMessage
    {
        public bool IsAllAlgorithmsFinished  { get; }

        public AlgorithmsFinishedStatusMessage(bool isAllFinished)
        {
            IsAllAlgorithmsFinished = isAllFinished;
        }
    }
}
