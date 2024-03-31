using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined
{
    internal class GraphAssembleDto
    {
        public IReadOnlyList<int> Dimensions { get; set; }

        public IReadOnlyCollection<VertexAssembleDto> Vertices { get; set; }

        public IReadOnlyDictionary<int, IReadOnlyList<VertexAssembleDto>> Neighborhood { get; set; }
    }
}
