namespace WPFVersion3D.Messages
{
    internal sealed class IsAllAlgorithmsFinishedMessage : PassValueMessage<bool>
    {
        public IsAllAlgorithmsFinishedMessage(bool isAllFinished) : base(isAllFinished)
        {
            
        }
    }
}
