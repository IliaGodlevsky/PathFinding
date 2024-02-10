using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Create
{
    internal class GraphStateCreateDto
    {
        public int AlgorithmRunId { get; set; }

        public IReadOnlyCollection<ICoordinate> Obstacles { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }

        public IReadOnlyCollection<int> Costs { get; set; }
    }
}
