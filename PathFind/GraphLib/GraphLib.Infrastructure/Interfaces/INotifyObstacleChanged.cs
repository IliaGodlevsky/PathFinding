using GraphLib.Infrastructure.EventHandlers;

namespace GraphLib.Infrastructure.Interfaces
{
    public interface INotifyObstacleChanged
    {
        event ObstacleChangedEventHandler ObstacleChanged;
    }
}
