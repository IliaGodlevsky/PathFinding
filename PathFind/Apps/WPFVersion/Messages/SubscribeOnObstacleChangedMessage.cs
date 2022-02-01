using GraphLib.Infrastructure.Interfaces;

namespace WPFVersion.Messages
{
    internal class SubscribeOnObstacleChangedMessage
    {
        public INotifyObstacleChanged Notifier { get; }

        public SubscribeOnObstacleChangedMessage(INotifyObstacleChanged notifier)
        {
            Notifier = notifier;
        }
    }
}
