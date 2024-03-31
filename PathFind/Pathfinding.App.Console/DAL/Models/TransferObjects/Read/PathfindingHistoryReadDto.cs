using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Read
{
    internal record PathfindingHistoryReadDto<T>
        where T : IVertex
    {
        public GraphReadDto<T> Graph { get; set; }

        public IReadOnlyCollection<AlgorithmRunHistoryReadDto> Algorithms { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }
    }
}
