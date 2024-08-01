using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public record PathfindingHistoryModel<T>
        where T : IVertex
    {
        public GraphModel<T> Graph { get; set; }

        public IReadOnlyCollection<AlgorithmRunHistoryModel> Algorithms { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }
    }
}
