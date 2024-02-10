﻿using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Read
{
    internal class GraphStateReadDto
    {
        public int AlgorithmRunId { get; set; }

        public IReadOnlyCollection<ICoordinate> Obstacles { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }

        public IReadOnlyCollection<int> Costs { get; set; }
    }
}
