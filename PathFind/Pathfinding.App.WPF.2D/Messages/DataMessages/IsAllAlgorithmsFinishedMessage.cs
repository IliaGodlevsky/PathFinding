namespace Pathfinding.App.WPF._2D.Messages.DataMessages
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
