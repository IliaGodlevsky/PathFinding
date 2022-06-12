using Algorithm.Interfaces;
using Common.Extensions;
using System.Diagnostics;

namespace ConsoleVersion.Extensions
{
    internal static class IGraphPathExtensions
    {
        public static string ToStatistics(this IGraphPath path, Stopwatch timer, int visited, string algorithm)
        {
            string pathfindingInfo = string.Format(MessagesTexts.PathfindingStatisticsFormat, path.Length, path.Cost, visited);
            return string.Join("\t", algorithm, timer.ToFormattedString(), pathfindingInfo);
        }
    }
}
