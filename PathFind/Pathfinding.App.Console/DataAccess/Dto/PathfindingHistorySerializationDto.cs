using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Dto
{
    internal class PathfindingHistorySerializationDto
    {
        public IGraph<Vertex> Graph { get; set; }

        public IReadOnlyCollection<AlgorithmSerializationDto> Algorithms { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }
    }
}
