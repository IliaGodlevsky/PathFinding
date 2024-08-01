using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class SubAlgorithmModel
    {
        public int Id { get; set; }

        public int AlgorithmRunId { get; set; }

        public int Order { get; set; }

        public IReadOnlyCollection<(ICoordinate Visited, IReadOnlyList<ICoordinate> Enqueued)> Visited { get; set; }

        public IReadOnlyCollection<ICoordinate> Path { get; set; }
    }
}
