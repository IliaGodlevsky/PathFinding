using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreateGraphStateRequest
    {
        public int AlgorithmRunId { get; set; }

        public IReadOnlyCollection<Coordinate> Obstacles { get; set; }

        public IReadOnlyCollection<Coordinate> Range { get; set; }

        public IReadOnlyCollection<int> Costs { get; set; }
    }
}
