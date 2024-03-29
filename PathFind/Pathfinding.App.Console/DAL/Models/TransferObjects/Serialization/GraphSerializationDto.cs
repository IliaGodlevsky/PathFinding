﻿using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization
{
    internal class GraphSerializationDto
    {
        public IReadOnlyList<int> DimensionSizes { get; set; }

        public IReadOnlyCollection<VertexSerializationDto> Vertices { get; set; }
    }
}
