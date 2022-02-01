using GraphLib.Infrastructure.Interfaces;

namespace WPFVersion.Messages
{
    internal class SubscribeOnCostChangedMessage
    {
        public INotifyVertexCostChanged Notifier { get; }

        public SubscribeOnCostChangedMessage(INotifyVertexCostChanged notifier)
        {
            Notifier = notifier;
        }
    }
}
