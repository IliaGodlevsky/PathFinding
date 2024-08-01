﻿namespace Pathfinding.Domain.Core
{
    public class PathfindingRange : IEntity<int>
    {
        public int Id { get; set; }

        public int GraphId { get; set; }

        public int VertexId { get; set; }

        public int Order { get; set; }
    }
}
