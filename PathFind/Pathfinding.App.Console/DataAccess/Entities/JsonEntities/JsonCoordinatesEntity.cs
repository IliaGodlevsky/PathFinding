using Pathfinding.App.Console.DataAccess.Repos;

namespace Pathfinding.App.Console.DataAccess.Entities.JsonEntities
{
    public class JsonCoordinatesEntity : IIdentityItem<long>
    {
        public long Id { get; set; }

        public long AlgorithmId { get; set; }

        public GraphJsonCoordinates[] Coordinates { get; set; }
    }
}
