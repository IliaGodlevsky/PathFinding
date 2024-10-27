using System;

namespace Pathfinding.Domain.Core
{
    public class Statistics : IEntity<int>
    {
        public int Id { get; set; }

        public int AlgorithmRunId { get; set; }

        public string Heuristics { get; set; } = null;

        public double? Weight { get; set; } = null;

        public string StepRule { get; set; } = null;

        public string ResultStatus { get; set; } = string.Empty;

        public double Elapsed { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }

        public int Visited { get; set; }
    }
}
