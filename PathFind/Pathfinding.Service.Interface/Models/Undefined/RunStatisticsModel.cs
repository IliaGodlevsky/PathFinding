using System;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public record RunStatisticsModel
    {
        public int Id { get; set; }

        public string AlgorithmName { get; set; }

        public string Heuristics { get; set; } = null;

        public double? Weight { get; set; } = null;

        public string StepRule { get; set; } = null;

        public int Visited { get; set; }

        public string ResultStatus { get; set; } = string.Empty;

        public TimeSpan Elapsed { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }
    }
}