using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class PathfindingHistoryCreateDto
    {
        public IGraph<Vertex> Graph { get; set; }

        public IReadOnlyCollection<AlgorithmCreateDto> Algorithms { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }
    }
}
