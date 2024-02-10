using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization
{
    internal class GraphStateSerializationDto
    {
        public IReadOnlyCollection<CoordinateDto> Obstacles { get; set; }

        public IReadOnlyCollection<CoordinateDto> Range { get; set; }

        public IReadOnlyCollection<int> Costs { get; set; }
    }
}
