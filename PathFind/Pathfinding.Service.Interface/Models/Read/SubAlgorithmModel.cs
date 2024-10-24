using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class SubAlgorithmModel
    {
        public int Id { get; set; }

        public int AlgorithmRunId { get; set; }

        public int Order { get; set; }

        public IReadOnlyCollection<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)> Visited { get; set; }

        public IReadOnlyCollection<Coordinate> Path { get; set; }
    }
}
