using Newtonsoft.Json;
using Pathfinding.App.Console.Serialization;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System.IO;

namespace Pathfinding.App.Console.Extensions
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteHistory(this BinaryWriter writer, SerializationInfo info)
        {
            writer.Write(info.Algorithms.Count);
            for (int i = 0; i < info.Algorithms.Count; i++)
            {
                writer.WriterCoordinates(info.Obstacles[i]);
                writer.WriterCoordinates(info.Visited[i]);
                writer.WriterCoordinates(info.Ranges[i]);
                writer.WriterCoordinates(info.Paths[i]);
                writer.WriteIntArray(info.Costs[i]);
                var statistics = info.Statistics[i];
                writer.Write(JsonConvert.SerializeObject(statistics));
            }
        }
    }
}
