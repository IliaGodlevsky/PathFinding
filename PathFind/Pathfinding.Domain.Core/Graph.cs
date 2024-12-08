﻿namespace Pathfinding.Domain.Core
{
    public class Graph : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Neighborhoods Neighborhood { get; set; }

        public SmoothLevels SmoothLevel { get; set; }

        public GraphStatuses Status { get; set; }

        public string Dimensions { get; set; }
    }
}
