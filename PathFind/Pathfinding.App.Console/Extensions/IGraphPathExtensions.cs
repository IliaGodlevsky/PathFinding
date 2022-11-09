using Pathfinding.AlgorithmLib.Core.Interface;
using System.Diagnostics;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IGraphPathExtensions
    {
        public static string ToStatistics(this IGraphPath path, Stopwatch timer, int visited, string algorithm)
        {
            string pathfindingInfo = string.Format(MessagesTexts.PathfindingStatisticsFormat, path.Count, path.Cost, visited);
            return string.Join("\t", algorithm, timer.Elapsed.ToString(@"mm\:ss\.fff"), pathfindingInfo);
        }
    }
}
