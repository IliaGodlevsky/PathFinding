using Pathfinding.Domain.Core;
using System;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class RunInfoModel
    {
        public int RunId { get; set; }

        public Algorithms Name { get; set; }

        public int Visited { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }

        public TimeSpan Elapsed { get; set; }

        public StepRules? StepRule { get; set; }

        public HeuristicFunctions? Heuristics { get; set; }

        public double? Weight { get; set; }

        public RunStatuses Status { get; set; }
    }
}
