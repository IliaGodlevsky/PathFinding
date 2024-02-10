using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization
{
    internal class VertexSerializationDto
    {
        public CoordinateDto Position { get; set; }

        public VertexCostDto Cost { get; set; }

        public bool IsObstacle { get; set; }

        public IReadOnlyCollection<CoordinateDto> Neighbors { get; set; }
    }
}
