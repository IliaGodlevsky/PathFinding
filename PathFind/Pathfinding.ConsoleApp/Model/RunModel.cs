using ReactiveUI;
using System;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class RunModel : ReactiveObject
    {
        public int RunId { get; set; }

        public string Name { get; set; }

        public int Visited { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }

        public TimeSpan Elapsed { get; set; }

        public string StepRule { get; set; }

        public string Heuristics { get; set; }

        public string Spread { get; set; }

        public string Status { get; set; }

        public object[] GetParametres()
        {
            return new object[] { };
        }
    }
}
