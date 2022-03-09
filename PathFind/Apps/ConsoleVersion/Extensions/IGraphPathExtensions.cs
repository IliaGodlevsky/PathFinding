using Algorithm.Extensions;
using Algorithm.Interfaces;
using System.Diagnostics;

namespace ConsoleVersion.Extensions
{
    internal static class IGraphPathExtensions
    {
        public static string ToStatistics(this IGraphPath path, Stopwatch timer, int visited, string algorithm)
        {
            return path.ToStatistics(timer, visited, algorithm, MessagesTexts.PathfindingStatisticsFormat);
        }
    }
}
