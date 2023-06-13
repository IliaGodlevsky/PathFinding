using Newtonsoft.Json;
using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System.IO;

namespace Pathfinding.App.Console.Extensions
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteHistory(this BinaryWriter writer, IPathfindingHistory history)
        {
            writer.Write(history.Algorithms.Count);
            foreach (var key in history.Algorithms)
            {
                writer.Write(key.ToByteArray());
                writer.WriterCoordinates(history.ObstacleVertices.Get(key));
                writer.WriterCoordinates(history.VisitedVertices.Get(key));
                writer.WriterCoordinates(history.RangeVertices.Get(key));
                writer.WriterCoordinates(history.PathVertices.Get(key));
                writer.WriteIntArray(history.Costs.Get(key));
                var statistics = history.Statistics.Get(key);
                writer.Write(JsonConvert.SerializeObject(statistics));
            }
        }
    }
}
