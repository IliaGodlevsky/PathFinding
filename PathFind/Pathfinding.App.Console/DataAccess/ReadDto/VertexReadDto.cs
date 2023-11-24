using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.ReadDto
{
    internal class VertexReadDto
    {
        public int Id { get; init; }

        public bool IsObstacles { get; init; }

        public int Cost { get; init; }

        public ICollection<VertexReadDto> Neighbours { get; init; }
    }
}
