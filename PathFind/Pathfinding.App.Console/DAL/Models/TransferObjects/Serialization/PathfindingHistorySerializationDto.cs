using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization
{
    internal class PathfindingHistorySerializationDto
    {
        public GraphSerializationDto Graph { get; set; }

        public IReadOnlyCollection<AlgorithmRunHistorySerializationDto> Algorithms { get; set; }

        public IReadOnlyCollection<CoordinateDto> Range { get; set; }
    }
}
