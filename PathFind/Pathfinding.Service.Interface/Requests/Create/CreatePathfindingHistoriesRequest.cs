using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreatePathfindingHistoriesRequest<T>
        where T : IVertex
    {
        public List<CreatePathfindingHistoryRequest<T>> Models { get; set; }
            = new List<CreatePathfindingHistoryRequest<T>>();
    }
}
