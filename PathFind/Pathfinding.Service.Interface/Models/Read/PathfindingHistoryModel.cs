using Pathfinding.Domain.Interface;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public record PathfindingHistoryModel<T>
        where T : IVertex
    {
        public GraphModel<T> Graph { get; set; }

        public IReadOnlyCollection<AlgorithmRunHistoryModel> Algorithms { get; set; }

        public IReadOnlyCollection<Coordinate> Range { get; set; }
    }
}
