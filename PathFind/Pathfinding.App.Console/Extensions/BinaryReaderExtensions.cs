using Pathfinding.App.Console.DAL.Models.TransferObjects;
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
                    Obstacles = reader.ReadCoordinates().ToReadOnly(),
                    Visited = reader.ReadCoordinates().ToReadOnly(),
                    Range = reader.ReadCoordinates().ToReadOnly(),
                    Path = reader.ReadCoordinates().ToReadOnly(),
                    Costs = reader.ReadIntArray().ToReadOnly(),
                    Statistics = reader.ReadStatisitics()
                };
            }
            return Array.AsReadOnly(algorithms);
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
