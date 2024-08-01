using System;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class RunStatisticsSerializationModel
    {
        public string Heuristics { get; set; } = null;

        public string StepRule { get; set; } = null;

        public string ResultStatus { get; set; } = string.Empty;

        public TimeSpan? AlgorithmSpeed { get; set; } = null;

        public TimeSpan? Elapsed { get; set; } = null;

        public int? Steps { get; set; } = null;

        public double? Cost { get; set; } = null;

        public int? Spread { get; set; } = null;
    }
}
