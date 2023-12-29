using Pathfinding.App.Console.DataAccess.Dto;
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
        public static void WriteHistory(this BinaryWriter writer,
            IReadOnlyCollection<AlgorithmSerializationDto> algorithms)
        {
            writer.Write(algorithms.Count);
            foreach (var algorithm in algorithms)
            {
                writer.WriteCoordinates(algorithm.Obstacles);
                writer.WriteCoordinates(algorithm.Visited);
                writer.WriteCoordinates(algorithm.Range);
                writer.WriteCoordinates(algorithm.Path);
                writer.WriteIntArray(algorithm.Costs);
                writer.WriteStatistics(algorithm.Statistics);
            }
        }

        public static void WriteNeighbours(this BinaryWriter writer, IReadOnlyDictionary<ICoordinate, IReadOnlyCollection<ICoordinate>> neighbours)
        {
            writer.WriteCoordinates(neighbours.Keys.ToReadOnly());
            foreach(var neighbour in neighbours.Values)
            {
                writer.WriteCoordinates(neighbour);
            }
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
    }
}
