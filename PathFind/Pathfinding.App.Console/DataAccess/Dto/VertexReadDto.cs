using Pathfinding.GraphLib.Core.Realizations;

namespace Pathfinding.App.Console.DataAccess.Dto
{
    internal class VertexReadDto
    {
        public int Id { get; set; }

        public int GraphId { get; set; }

        public bool IsObstacle { get; set; }

        public VertexCost Cost { get; set; }

        public Coordinate Coordinate { get; set; }
    }
}
