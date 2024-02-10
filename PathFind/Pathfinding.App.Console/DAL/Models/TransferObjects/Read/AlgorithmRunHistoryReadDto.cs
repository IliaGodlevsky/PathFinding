using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Read
{
    internal class AlgorithmRunHistoryReadDto
    {
        public AlgorithmRunReadDto Run { get; set; }

        public IReadOnlyCollection<SubAlgorithmReadDto> SubAlgorithms { get; set; }

        public RunStatisticsDto Statistics { get; set; }

        public GraphStateReadDto GraphState { get; set; }
    }
}
