using Newtonsoft.Json;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Shared.Extensions;
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
                writer.Write(key.ToByteArray());
                writer.WriteCoordinates(history.Obstacles.TryGetOrAddNew(key));
                writer.WriteCoordinates(history.Visited.TryGetOrAddNew(key));
                writer.WriteCoordinates(history.Ranges.TryGetOrAddNew(key));
                writer.WriteCoordinates(history.Paths.TryGetOrAddNew(key));
                writer.WriteIntArray(history.Costs.TryGetOrAddNew(key));
                var statistics = history.Statistics.GetOrDefault(key, new Statistics());
                writer.Write(JsonConvert.SerializeObject(statistics));
            }
        }
    }
}
