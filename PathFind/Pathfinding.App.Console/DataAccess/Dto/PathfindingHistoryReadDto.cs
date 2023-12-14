using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Dto
{
    internal class PathfindingHistoryReadDto
    {
        public int Id { get; set; }

        public IGraph<Vertex> Graph { get; set; }

        public IReadOnlyCollection<AlgorithmReadDto> Algorithms { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }
    }
}
