using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class AlgorithmRunHistoryModel
    {
        public AlgorithmRunModel Run { get; set; }

        public IReadOnlyCollection<SubAlgorithmModel> SubAlgorithms { get; set; }

        public RunStatisticsModel Statistics { get; set; }

        public GraphStateModel GraphState { get; set; }
    }
}
