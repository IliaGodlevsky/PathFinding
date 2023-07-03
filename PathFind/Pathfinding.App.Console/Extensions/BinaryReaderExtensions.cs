using Newtonsoft.Json;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Shared.Extensions;
using System;
using System.IO;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class BinaryReaderExtensions
    {
        private const int GuidSize = 16;

        public static GraphPathfindingHistory ReadHistory(this BinaryReader reader, ICoordinateFactory factory)
        {
            var history = new GraphPathfindingHistory();
            int keyCount = reader.ReadInt32();
            history.PathfindingRange.AddRange(reader.ReadCoordinates(factory));
            for (int i = 0; i < keyCount; i++)
            {
                var key = new Guid(reader.ReadBytes(GuidSize));
                history.Algorithms.Add(key);
                history.Obstacles.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates(factory));
                history.Visited.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates(factory));
                history.Ranges.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates(factory));
                history.Paths.TryGetOrAddNew(key).AddRange(reader.ReadCoordinates(factory));
                history.Costs.TryGetOrAddNew(key).AddRange(reader.ReadIntArray());
                var statistics = JsonConvert.DeserializeObject<Statistics>(reader.ReadString()); 
                history.Statistics.Add(key, statistics);
            }
            Enumerable.Range(0, reader.ReadInt32())
                .ForEach(i => history.SmoothHistory.Push(reader.ReadIntArray()));
            return history;
        }
    }
}
