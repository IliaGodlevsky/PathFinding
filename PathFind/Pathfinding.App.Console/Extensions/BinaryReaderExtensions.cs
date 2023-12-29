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
    internal static class BinaryReaderExtensions
    {
        public static IReadOnlyCollection<AlgorithmSerializationDto> ReadAlgorithm(this BinaryReader reader)
        {
            int algorithmsCount = reader.ReadInt32();
            var algorithms = new AlgorithmSerializationDto[algorithmsCount];
            for (int algorithm = 0; algorithm < algorithmsCount; algorithm++)
            {
                algorithms[algorithm] = new()
                {
                    Obstacles = Array.AsReadOnly(reader.ReadCoordinates()),
                    Visited = Array.AsReadOnly(reader.ReadCoordinates()),
                    Range = Array.AsReadOnly(reader.ReadCoordinates()),
                    Path = Array.AsReadOnly(reader.ReadCoordinates()),
                    Costs = Array.AsReadOnly(reader.ReadIntArray()),
                    Statistics = reader.ReadStatisitics()
                };
            }
            return Array.AsReadOnly(algorithms);
        }

        public static IReadOnlyDictionary<ICoordinate, IReadOnlyCollection<ICoordinate>> ReadNeighbours(this BinaryReader reader)
        {
            var result = new Dictionary<ICoordinate, IReadOnlyCollection<ICoordinate>>();
            var coordinates = reader.ReadCoordinates();
            foreach (var coordinate in coordinates)
            {
                var neighbours = reader.ReadCoordinates().ToReadOnly();
                result.Add(coordinate, neighbours);
            }
            return result.AsReadOnly();
        }

        private static int? ReadNullableInt32(this BinaryReader reader)
        {
            bool hasValue = reader.ReadBoolean();
            if (hasValue)
            {
                return reader.ReadInt32();
            }
            return null;
        }

        private static double? ReadNullableDouble(this BinaryReader reader)
        {
            bool hasValue = reader.ReadBoolean();
            if (hasValue)
            {
                return reader.ReadDouble();
            }
            return null;
        }

        private static string ReadNullableString(this BinaryReader reader)
        {
            bool hasValue = reader.ReadBoolean();
            if (hasValue)
            {
                return reader.ReadString();
            }
            return string.Empty;
        }

        private static TimeSpan? ReadNullableTimeSpan(this BinaryReader reader)
        {
            bool hasValue = reader.ReadBoolean();
            if (hasValue)
            {
                return TimeSpan.FromMilliseconds(reader.ReadDouble());
            }
            return null;
        }

        private static Statistics ReadStatisitics(this BinaryReader reader)
        {
            return new(reader.ReadString())
            {
                ResultStatus = reader.ReadString(),
                Elapsed = reader.ReadNullableTimeSpan(),
                Visited = reader.ReadNullableInt32(),
                Cost = reader.ReadNullableDouble(),
                Steps = reader.ReadNullableInt32(),
                StepRule = reader.ReadNullableString(),
                Heuristics = reader.ReadNullableString(),
                Spread = reader.ReadNullableInt32()
            };
        }
    }
}
