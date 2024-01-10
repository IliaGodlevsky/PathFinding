using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class PathfindingHistoryJsonSerializationDto
    {
        public GraphJsonSerializationDto Graph { get; set; }

        public IReadOnlyCollection<AlgorithmJsonSerializationDto> Algorithms { get; set; }

        public IReadOnlyCollection<CoordinateDto> Range { get; set; }
    }
}
