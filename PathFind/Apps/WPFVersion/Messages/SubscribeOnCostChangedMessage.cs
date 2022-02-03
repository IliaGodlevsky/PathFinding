using GraphLib.Infrastructure;

namespace WPFVersion.Messages
{
    internal class SubscribeOnCostChangedMessage
    {
        public INotifyCostChanged Notifier { get; }

        public SubscribeOnCostChangedMessage(INotifyCostChanged notifier)
        {
            Notifier = notifier;
        }
    }
}
