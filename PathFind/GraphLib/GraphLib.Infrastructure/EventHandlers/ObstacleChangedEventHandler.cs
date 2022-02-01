using GraphLib.Infrastructure.EventArguments;

namespace GraphLib.Infrastructure.EventHandlers
{
    public delegate void ObstacleChangedEventHandler(object sender, BaseVertexChangedEventArgs<bool> e);
}
