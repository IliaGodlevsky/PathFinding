using GraphLib.Interfaces;

namespace GraphLib.Infrastructure.EventArguments
{
    public class ObstacleChangedEventArgs : BaseVertexChangedEventArgs<bool>
    {
        public ObstacleChangedEventArgs(IVertex vertex) : base(vertex.IsObstacle, vertex)
        {

        }
    }
}
