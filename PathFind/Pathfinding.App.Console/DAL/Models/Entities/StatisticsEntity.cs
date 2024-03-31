using System;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    internal class StatisticsEntity
    {
        public int Id { get; set; }

        public int AlgorithmRunId { get; set; }

        public string Heuristics { get; set; } = null;

        public string StepRule { get; set; } = null;

        public string ResultStatus { get; set; } = string.Empty;

        public TimeSpan? AlgorithmSpeed { get; set; } = null!;

        public TimeSpan? Elapsed { get; set; } = null;

        public int? Steps { get; set; } = null;

        public double? Cost { get; set; } = null;

        public int? Visited { get; set; } = null;

        public int? Spread { get; set; } = null;
    }
}
