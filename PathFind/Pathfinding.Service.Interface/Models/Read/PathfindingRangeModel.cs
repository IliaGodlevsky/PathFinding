﻿using Pathfinding.Shared.Primitives;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class PathfindingRangeModel
    {
        public int Id { get; set; }

        public bool IsSource { get; set; }

        public bool IsTarget { get; set; }

        public int VertexId { get; set; }

        public int GraphId { get; set; }

        public int Order { get; set; }

        public Coordinate Position { get; set; }
    }
}
