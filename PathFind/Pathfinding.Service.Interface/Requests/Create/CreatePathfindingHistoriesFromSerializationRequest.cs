using Pathfinding.Service.Interface.Models.Serialization;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreatePathfindingHistoriesFromSerializationRequest
    {
        public List<PathfindingHistorySerializationModel> Models { get; set; }
            = new List<PathfindingHistorySerializationModel>();
    }
}
