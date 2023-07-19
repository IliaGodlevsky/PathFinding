using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.App.Console.Extensions
{
    internal static class BinaryReaderExtensions
    {
        public static GraphPathfindingHistory ReadHistory(this BinaryReader reader, ICoordinateFactory factory)
        {
            var history = new GraphPathfindingHistory();
            int keyCount = reader.ReadInt32();
            history.PathfindingRange.AddRange(reader.ReadCoordinates(factory));
            for (int i = 0; i < keyCount; i++)
            {
                var key = reader.ReadGuid();
                history.Algorithms.Add(key);
                history.Obstacles.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates(factory));
                history.Visited.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates(factory));
                history.Ranges.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates(factory));
                history.Paths.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates(factory));
                history.Costs.TryGetOrAddNew(key).AddRange(reader.ReadIntArray());
                history.Statistics.Add(key, reader.ReadStatisitics());
            }
            reader.ReadSmoothHistory().ForEach(history.SmoothHistory.Push);
            return history;
        }

        private static IEnumerable<IReadOnlyList<int>> ReadSmoothHistory(this BinaryReader reader)
        {
            int count = reader.ReadInt32();
            while (count-- > 0)
            {
                yield return reader.ReadIntArray();
            }
        }

        private static Guid ReadGuid(this BinaryReader reader)
        {
            return new(reader.ReadBytes(16));
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
            return new()
            {
                Algorithm = (Algorithms)reader.ReadInt32(),
                ResultStatus = (AlgorithmResultStatus)reader.ReadInt32(),
                Elapsed = reader.ReadNullableTimeSpan(),
                Visited = reader.ReadNullableInt32(),
                Cost = reader.ReadNullableDouble(),
                Steps = reader.ReadNullableInt32(),
                StepRule = (StepRules?)reader.ReadNullableInt32(),
                Heuristics = (Heuristics?)reader.ReadNullableInt32(),
                Spread = reader.ReadNullableInt32()
            };
        }
    }
}
