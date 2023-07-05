using Pathfinding.App.Console.Localization;
using System;
using System.Globalization;

namespace Pathfinding.App.Console.Model.Notes
{
    internal class Statistics
    {
        public static readonly Statistics Empty = new Statistics();

        private const int IntPadding = 10;

        private readonly string NoteFormat = Languages.PathfindingResultFormat;
        private readonly string TimeFormat = @"mm\:ss\.fff";
        private readonly string StatsFormat = Languages.StatisticsFormat;

        public string AlgorithmName { get; set; } = string.Empty;

        public TimeSpan Elapsed { get; set; } = TimeSpan.Zero;

        public int VisitedVertices { get; set; } = default;

        public int Steps { get; set; } = default;

        public double Cost { get; set; } = default;

        public override string ToString()
        {
            string algorithmName = AlgorithmName.PadRight(totalWidth: 30);
            int costValue = (int)Cost;
            string steps = string.Empty;
            string cost = string.Empty;
            string elapsed = string.Empty;
            string visited = string.Empty;
            if (Steps != 0)
            {
                steps = Steps.ToString().PadRight(IntPadding);
            }
            if (costValue != 0)
            {
                cost = costValue.ToString().PadRight(IntPadding);
            }
            if (Elapsed != TimeSpan.Zero)
            {
                elapsed = Elapsed.ToString(TimeFormat, CultureInfo.InvariantCulture);
            }
            if (VisitedVertices != 0)
            {
                visited = VisitedVertices.ToString().PadRight(IntPadding);
            }
            string result = string.Format(StatsFormat, algorithmName, elapsed, visited);
            return string.Format(NoteFormat, result, steps, cost);
        }
    }
}
