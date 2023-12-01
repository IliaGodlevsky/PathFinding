using Pathfinding.App.Console.DataAccess;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Extensions
{
    internal static class GraphsPathfindingHistoryExtensions
    {
        public static void Merge(this GraphsPathfindingHistory history,
            GraphsPathfindingHistory toAdd, IReadOnlyCollection<int> graphIds)
        {
            foreach (var graphKey in graphIds)
            {
                var graph = toAdd.GetGraph(graphKey);
                history.Add(graph);
                var historyToAdd = toAdd.GetHistory(graphKey);
                history.Add(graphKey, historyToAdd);
                var range = history.GetRange(graphKey);
                range.AddRange(toAdd.GetRange(graphKey));
                var smoothHistory = history.GetSmoothHistory(graphKey);
                toAdd.GetSmoothHistory(graphKey).ForEach(smoothHistory.Push);
            }
        }

        public static void Merge(this GraphsPathfindingHistory history,
            GraphsPathfindingHistory toAdd)
        {
            history.Merge(toAdd, toAdd.Ids);
        }
    }
}
