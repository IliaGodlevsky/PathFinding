using Pathfinding.Domain.Core;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreateStatisticsRequest
    {
        public int GraphId { get; set; }

        public Domain.Core.Algorithms Algorithm { get; set; }

        public HeuristicFunctions? Heuristics { get; set; } = null;

        public double? Weight { get; set; } = null;

        public StepRules? StepRule { get; set; } = null;

        public int Visited { get; set; }

        public RunStatuses ResultStatus { get; set; }

        public TimeSpan Elapsed { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }
    }
}
