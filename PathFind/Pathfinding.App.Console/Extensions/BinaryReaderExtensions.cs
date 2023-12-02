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
    internal static class BinaryReaderExtensions
    {
        public static GraphPathfindingHistory ReadHistory(this BinaryReader reader)
        {
            var history = new GraphPathfindingHistory();
            int keyCount = reader.ReadInt32();
            for (int i = 0; i < keyCount; i++)
            {
                var key = reader.ReadInt32();
                history.Algorithms.Add(key);
                history.Obstacles.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates());
                history.Visited.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates());
                history.Ranges.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates());
                history.Paths.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates());
                history.Costs.TryGetOrAddNew(key).AddRange(reader.ReadIntArray());
                history.Statistics.Add(key, reader.ReadStatisitics());
            }
            return history;
        }

        public static IEnumerable<IReadOnlyList<int>> ReadSmoothHistory(this BinaryReader reader)
        {
            int count = reader.ReadInt32();
            while (count-- > 0)
            {
                yield return reader.ReadIntArray();
            }
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
