using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization
{
    internal class AlgorithmRunHistorySerializationDto
    {
        public AlgorithmRunSerializationDto Run { get; set; }

        public RunStatisticsSerializationDto Statistics { get; set; }

        public IReadOnlyCollection<SubAlgorithmSerializationDto> SubAlgorithms { get; set; }

        public GraphStateSerializationDto GraphState { get; set; }
    }
}
