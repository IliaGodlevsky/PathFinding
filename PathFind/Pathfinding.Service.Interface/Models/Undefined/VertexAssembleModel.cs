using Pathfinding.Domain.Interface;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public class VertexAssembleModel
    {
        public int Id { get; set; }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public ICoordinate Coordinate { get; set; }
    }
}
