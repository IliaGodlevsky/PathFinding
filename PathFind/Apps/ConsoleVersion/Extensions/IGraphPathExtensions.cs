﻿using Algorithm.Interfaces;
using Common.Extensions;
using System.Diagnostics;

namespace ConsoleVersion.Extensions
{
    internal static class IGraphPathExtensions
    {
        public static string ToStatistics(this IGraphPath path, Stopwatch timer, int visited, string algorithm)
        {
            string timerInfo = timer.ToFormattedString();
            var pathfindingInfos = new object[] { path.Length, path.Cost, visited };
            string pathfindingInfo = string.Format(MessagesTexts.PathfindingStatisticsFormat, pathfindingInfos);
            return string.Join("\t", algorithm, timerInfo, pathfindingInfo);
        }
    }
}