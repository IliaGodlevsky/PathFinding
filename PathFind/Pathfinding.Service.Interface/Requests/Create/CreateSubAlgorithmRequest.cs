using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreateSubAlgorithmRequest
    {
        public int AlgorithmRunId { get; set; }

        public int Order { get; set; }

        public IReadOnlyCollection<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)> Visited { get; set; }

        public IReadOnlyCollection<Coordinate> Path { get; set; }
    }
}
