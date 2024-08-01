using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreateSubAlgorithmRequest
    {
        public int AlgorithmRunId { get; set; }

        public int Order { get; set; }

        public IReadOnlyCollection<(ICoordinate Visited, IReadOnlyList<ICoordinate> Enqueued)> Visited { get; set; }

        public IReadOnlyCollection<ICoordinate> Path { get; set; }
    }
}
