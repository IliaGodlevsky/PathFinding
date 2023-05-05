using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Localization;
using System;
using System.Globalization;

namespace Pathfinding.App.Console.Model.Notes
{
    internal class StatisticsNote
    {
        protected const int IntPadding = 10;

        private readonly string TimeFormat = @"mm\:ss\.fff";
        private readonly string StatsFormat = Languages.StatisticsFormat;

        public string AlgorithmName { get; set; }

        public TimeSpan Elapsed { get; set; }

        public int VisitedVertices { get; set; }

        public override string ToString()
        {
            string algorithmName = AlgorithmName.PadRight(totalWidth: 35);
            string elapsed = Elapsed.ToString(TimeFormat, CultureInfo.InvariantCulture);
            string visited = VisitedVertices.ToString().PadRight(IntPadding);
            return string.Format(StatsFormat, algorithmName, elapsed, visited);
        }
    }
}
