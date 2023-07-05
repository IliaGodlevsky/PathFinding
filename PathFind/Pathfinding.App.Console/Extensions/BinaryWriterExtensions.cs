using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Shared.Extensions;
using System;
using System.IO;

namespace Pathfinding.App.Console.Extensions
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteHistory(this BinaryWriter writer, GraphPathfindingHistory history)
        {
            writer.Write(history.Algorithms.Count);
            writer.WriteCoordinates(history.PathfindingRange);
            foreach (var key in history.Algorithms)
            {
                writer.WriteGuid(key);
                writer.WriteCoordinates(history.Obstacles.TryGetOrAddNew(key));
                writer.WriteCoordinates(history.Visited.TryGetOrAddNew(key));
                writer.WriteCoordinates(history.Ranges.TryGetOrAddNew(key));
                writer.WriteCoordinates(history.Paths.TryGetOrAddNew(key));
                writer.WriteIntArray(history.Costs.TryGetOrAddNew(key));
                var statistics = history.Statistics.GetOrDefault(key, Statistics.Empty);
                writer.WriteStatistics(statistics);
            }
            writer.Write(history.SmoothHistory.Count);
            history.SmoothHistory.ForEach(writer.WriteIntArray);
        }

        private static void WriteStatistics(this BinaryWriter writer, Statistics statistics)
        {
            writer.Write(statistics.AlgorithmName);
            writer.Write(statistics.Elapsed.TotalMilliseconds);
            writer.Write(statistics.VisitedVertices);
            writer.Write(statistics.Cost);
            writer.Write(statistics.Steps);
        }

        private static void WriteGuid(this BinaryWriter writer, Guid guid)
        {
            writer.Write(guid.ToByteArray());
        }
    }
}
