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
            writer.WriteEnum(statistics.Algorithm);
            writer.WriteEnum(statistics.ResultStatus);
            writer.WriteNullableTimeSpan(statistics.Elapsed);
            writer.WriteNullableInt32(statistics.Visited);
            writer.WriteNullableDouble(statistics.Cost);
            writer.WriteNullableInt32(statistics.Steps);
            writer.WriteNullableEnum(statistics.StepRule);
            writer.WriteNullableEnum(statistics.Heuristics);
            writer.WriteNullableInt32(statistics.Spread);
        }

        private static void WriteEnum<T>(this BinaryWriter writer, T value)
            where T : struct, Enum
        {
            string val = value.ToString();
            writer.Write(val);
        }

        public static void WriteNullableEnum<T>(this BinaryWriter writer, T? value)
            where T : struct, Enum
        {
            writer.Write(value.HasValue);
            if (value.HasValue)
            {
                writer.WriteEnum(value.Value);
            }
        }

        private static void WriteNullableInt32(this BinaryWriter writer, int? value)
        {
            writer.Write(value.HasValue);
            if (value.HasValue)
            {
                writer.Write(value.Value);
            }
        }

        private static void WriteNullableTimeSpan(this BinaryWriter writer, TimeSpan? value)
        {
            writer.Write(value.HasValue);
            if (value.HasValue)
            {
                writer.Write(value.Value.TotalMilliseconds);
            }
        }

        private static void WriteNullableDouble(this BinaryWriter writer, double? value)
        {
            writer.Write(value.HasValue);
            if (value.HasValue)
            {
                writer.Write(value.Value);
            }
        }

        private static void WriteGuid(this BinaryWriter writer, Guid guid)
        {
            writer.Write(guid.ToByteArray());
        }
    }
}
