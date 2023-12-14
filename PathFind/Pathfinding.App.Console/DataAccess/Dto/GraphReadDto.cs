using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Dto
{
    internal class GraphReadDto
    {
        public int Id { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public IReadOnlyCollection<VertexReadDto> Vertices { get; set; }

        public IReadOnlyDictionary<int, IReadOnlyCollection<VertexReadDto>> Neighborhood { get; set; }
    }
}
