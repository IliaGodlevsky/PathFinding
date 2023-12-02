using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.App.Console.Extensions
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteHistory(this BinaryWriter writer, GraphPathfindingHistory history)
        {
            writer.Write(history.Algorithms.Count);
            foreach (var key in history.Algorithms)
            {
                writer.Write(key);
                writer.WriteCoordinates(history.Obstacles, key);
                writer.WriteCoordinates(history.Visited, key);
                writer.WriteCoordinates(history.Ranges, key);
                writer.WriteCoordinates(history.Paths, key);
                writer.WriteIntArray(history.Costs.GetOrEmpty(key));
                var statistics = history.Statistics.GetOrDefault(key, Statistics.Empty);
                writer.WriteStatistics(statistics);
            }
        }

        private static void WriteCoordinates(this BinaryWriter writer,
            Dictionary<int, List<ICoordinate>> dict, int key)
        {
            writer.WriteCoordinates(dict.GetOrEmpty(key));
        }

        private static void WriteNullableString(this BinaryWriter writer, string value)
        {
            bool hasValue = !string.IsNullOrEmpty(value);
            writer.Write(hasValue);
            if (hasValue)
            {
                writer.Write(value);
            }
        }

        private static void WriteStatistics(this BinaryWriter writer, Statistics statistics)
        {
            writer.Write(statistics.Algorithm);
            writer.Write(statistics.ResultStatus);
            writer.WriteNullableTimeSpan(statistics.Elapsed);
            writer.WriteNullableInt32(statistics.Visited);
            writer.WriteNullableDouble(statistics.Cost);
            writer.WriteNullableInt32(statistics.Steps);
            writer.WriteNullableString(statistics.StepRule);
            writer.WriteNullableString(statistics.Heuristics);
            writer.WriteNullableInt32(statistics.Spread);
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
