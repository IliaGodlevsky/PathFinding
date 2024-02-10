using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization
{
    internal class SubAlgorithmSerializationDto
    {
        public int Order { get; set; }

        public IReadOnlyCollection<CoordinateDto> Path { get; set; }

        public IReadOnlyCollection<VisitedVerticesDto> Visited { get; set; }
    }
}
