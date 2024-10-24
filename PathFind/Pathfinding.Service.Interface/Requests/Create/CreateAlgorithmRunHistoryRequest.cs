using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreateAlgorithmRunHistoryRequest
    {
        public CreateAlgorithmRunRequest Run { get; set; }

        public IReadOnlyCollection<CreateSubAlgorithmRequest> SubAlgorithms { get; set; }

        public CreateGraphStateRequest GraphState { get; set; }

        public RunStatisticsModel Statistics { get; set; }
    }
}
