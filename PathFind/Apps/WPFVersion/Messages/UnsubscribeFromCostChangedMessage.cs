using GraphLib.Infrastructure;

namespace WPFVersion.Messages
{
    internal class UnsubscribeFromCostChangedMessage
    {
        public INotifyCostChanged Notifier { get; }

        public UnsubscribeFromCostChangedMessage(INotifyCostChanged notifier)
        {
            Notifier = notifier;
        }
    }
}
