using Pathfinding.Domain.Interface;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public class VertexAssembleModel
    {
        public int Id { get; set; }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public Coordinate Coordinate { get; set; }
    }
}
