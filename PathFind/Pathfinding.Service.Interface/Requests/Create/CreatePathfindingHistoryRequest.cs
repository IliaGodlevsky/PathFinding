using Pathfinding.Domain.Interface;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreatePathfindingHistoryRequest<T>
        where T : IVertex
    {
        public CreateGraphRequest<T> Graph { get; set; }

        public IReadOnlyCollection<CreateStatisticsRequest> Statistics { get; set; }

        public IReadOnlyCollection<Coordinate> Range { get; set; }
    }
}
