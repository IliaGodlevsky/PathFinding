using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class GraphStateModel
    {
        public int AlgorithmRunId { get; set; }

        public IReadOnlyCollection<Coordinate> Regulars { get; set; }

        public IReadOnlyCollection<Coordinate> Obstacles { get; set; }

        public IReadOnlyCollection<Coordinate> Range { get; set; }

        public IReadOnlyCollection<(Coordinate Position, int Cost)> Costs { get; set; }
    }
}
