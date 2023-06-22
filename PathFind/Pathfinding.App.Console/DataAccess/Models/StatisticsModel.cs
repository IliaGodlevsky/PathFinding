using Pathfinding.App.Console.Localization;
using System;
using System.Globalization;

namespace Pathfinding.App.Console.DataAccess.Models
{
    internal class StatisticsModel : IIdentityItem<long>
    {
        private const int IntPadding = 10;

        private readonly string NoteFormat = Languages.PathfindingResultFormat;
        private readonly string TimeFormat = @"mm\:ss\.fff";
        private readonly string StatsFormat = Languages.StatisticsFormat;

        public long Id { get; set; }

        public long AlgorithmId { get; set; }

        public string AlgorithmName { get; set; }

        public TimeSpan Elapsed { get; set; }

        public int Visited { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }

        public override string ToString()
        {
            int costValue = (int)Cost;
            string steps = Steps.ToString().PadRight(IntPadding);
            string cost = costValue.ToString().PadRight(IntPadding);
            string algorithmName = AlgorithmName.PadRight(totalWidth: 25);
            string elapsed = Elapsed.ToString(TimeFormat, CultureInfo.InvariantCulture);
            string visited = Visited.ToString().PadRight(IntPadding);
            string result = string.Format(StatsFormat, algorithmName, elapsed, visited);
            return string.Format(NoteFormat, result, steps, cost);
        }
    }
}
