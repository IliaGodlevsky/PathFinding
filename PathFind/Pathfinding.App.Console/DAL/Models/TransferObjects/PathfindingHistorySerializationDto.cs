using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class PathfindingHistorySerializationDto
    {
        public GraphSerializationDto Graph { get; set; }

        public IReadOnlyCollection<AlgorithmSerializationDto> Algorithms { get; set; }

        public IReadOnlyCollection<CoordinateDto> Range { get; set; }
    }
}
