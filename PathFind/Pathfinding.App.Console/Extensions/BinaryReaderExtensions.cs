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

        private static TimeSpan ReadTimeSpan(this BinaryReader reader)
        {
            return TimeSpan.FromMilliseconds(reader.ReadDouble());
        }

        private static Statistics ReadStatisitics(this BinaryReader reader)
        {
            return new()
            {
                AlgorithmName = reader.ReadString(),
                Elapsed = reader.ReadTimeSpan(),
                VisitedVertices = reader.ReadInt32(),
                Cost = reader.ReadDouble(),
                Steps = reader.ReadInt32(),
            };
        }
    }
}
