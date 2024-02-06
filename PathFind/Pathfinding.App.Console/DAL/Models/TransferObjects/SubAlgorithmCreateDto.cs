using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class SubAlgorithmCreateDto
    {
        public int AlgorithmId { get;set; }

        public int Order { get; set; }

        public IReadOnlyCollection<(ICoordinate Visited, IReadOnlyList<ICoordinate> Enqueued)> Visited { get; set; }

        public IReadOnlyCollection<ICoordinate> Path { get; set; }

    }
}
