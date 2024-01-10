using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class VertexJsonSerializationDto
    {
        public CoordinateDto Position { get; set; }

        public VertexCostDto Cost { get; set; }

        public bool IsObstacle { get; set; }

        public IReadOnlyCollection<CoordinateDto> Neighbors { get; set; }
    }
}
