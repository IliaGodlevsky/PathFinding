using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Read
{
    internal class RunVisualizationDto
    {
        public TimeSpan? AlgorithmSpeed { get; set; }

        public GraphStateReadDto GraphState { get; set; }

        public IReadOnlyCollection<SubAlgorithmReadDto> Algorithms { get; set; }
    }
}
