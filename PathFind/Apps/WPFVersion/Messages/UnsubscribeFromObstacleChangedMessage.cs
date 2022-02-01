using GraphLib.Infrastructure.Interfaces;

namespace WPFVersion.Messages
{
    internal class UnsubscribeFromObstacleChangedMessage
    {
        public INotifyObstacleChanged Notifier { get; }

        public UnsubscribeFromObstacleChangedMessage(INotifyObstacleChanged notifier)
        {
            Notifier = notifier;
        }
    }
}
