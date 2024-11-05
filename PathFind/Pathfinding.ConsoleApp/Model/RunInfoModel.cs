using ReactiveUI;
using System;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class RunInfoModel
    {
        public int RunId { get; set; }

        public string Name { get; set; }

        public int Visited { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }

        public TimeSpan Elapsed { get; set; }

        public string StepRule { get; set; }

        public string Heuristics { get; set; }

        public string Weight { get; set; }

        public string Status { get; set; }
    }
}
