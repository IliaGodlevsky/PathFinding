using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization
{
    internal record GraphSerializationDto
    {
        public string Name { get; set; }

        public IReadOnlyList<int> DimensionSizes { get; set; }

        public IReadOnlyCollection<VertexSerializationDto> Vertices { get; set; }
    }
}
