using GraphLib.Infrastructure.Interfaces;

namespace WPFVersion.Messages
{
    internal class UnsubscribeFromCostChangedMessage
    {
        public INotifyVertexCostChanged Notifier { get; }

        public UnsubscribeFromCostChangedMessage(INotifyVertexCostChanged notifier)
        {
            Notifier = notifier;
        }
    }
}
