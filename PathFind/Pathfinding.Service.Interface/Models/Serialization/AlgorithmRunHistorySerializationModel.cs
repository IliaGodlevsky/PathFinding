using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class AlgorithmRunHistorySerializationModel
    {
        public AlgorithmRunSerializationModel Run { get; set; }

        public RunStatisticsSerializationModel Statistics { get; set; }

        public IReadOnlyCollection<SubAlgorithmSerializationModel> SubAlgorithms { get; set; }

        public GraphStateSerializationModel GraphState { get; set; }
    }
}
