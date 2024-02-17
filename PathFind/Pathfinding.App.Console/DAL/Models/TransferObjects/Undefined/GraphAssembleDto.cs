using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined
{
    internal class GraphAssembleDto
    {
        public int Width { get; set; }

        public int Length { get; set; }

        public IReadOnlyCollection<VertexAssembleDto> Vertices { get; set; }

        public IReadOnlyDictionary<int, IReadOnlyList<VertexAssembleDto>> Neighborhood { get; set; }
    }
}
