namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class IsAllAlgorithmsFinishedMessage : PassValueMessage<bool>
    {
        public IsAllAlgorithmsFinishedMessage(bool isAllFinished) : base(isAllFinished)
        {

        }
    }
}
