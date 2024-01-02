using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class PathfindingHistorySerializationDto
    {
        public GraphSerializationDto Graph { get; set; }

        public IReadOnlyCollection<AlgorithmSerializationDto> Algorithms { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }
    }
}
