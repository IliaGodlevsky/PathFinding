namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class IsAllAlgorithmsFinishedMessage : PassValueMessage<bool>
    {
        public IsAllAlgorithmsFinishedMessage(bool isAllFinished) : base(isAllFinished)
        {

        }
    }
}
