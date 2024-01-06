using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class GraphAssembleDto
    {
        public int Width { get; set; }

        public int Length { get; set; }

        public IReadOnlyCollection<VertexAssembleDto> Vertices { get; set; }

        public IReadOnlyDictionary<int, IReadOnlyCollection<VertexAssembleDto>> Neighborhood { get; set; }
    }
}
