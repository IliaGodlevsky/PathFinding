using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Create
{
    internal class AlgorithmRunHistoryCreateDto
    {
        public AlgorithmRunCreateDto Run { get; set; }

        public IReadOnlyCollection<SubAlgorithmCreateDto> SubAlgorithms { get; set; }

        public GraphStateCreateDto GraphState { get; set; }

        public RunStatisticsDto Statistics { get; set; }
    }
}
