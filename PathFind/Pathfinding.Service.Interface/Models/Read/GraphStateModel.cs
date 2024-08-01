using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class GraphStateModel
    {
        public int AlgorithmRunId { get; set; }

        public IReadOnlyCollection<ICoordinate> Obstacles { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }

        public IReadOnlyCollection<int> Costs { get; set; }
    }
}
