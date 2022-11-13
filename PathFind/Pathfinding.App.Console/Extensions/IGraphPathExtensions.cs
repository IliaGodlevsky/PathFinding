using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using System.Diagnostics;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IGraphPathExtensions
    {
        public static string ToStatistics(this IGraphPath path, Stopwatch timer, int visited, PathfindingProcess algorithm)
        {
            var time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            string pathfindingInfo = string.Format(MessagesTexts.PathfindingStatisticsFormat, path.Count, path.Cost, visited);
            return string.Join("\t", algorithm, time, pathfindingInfo);
        }
    }
}
