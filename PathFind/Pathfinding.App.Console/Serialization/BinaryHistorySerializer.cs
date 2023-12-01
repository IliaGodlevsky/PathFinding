using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using System.IO;
using System.Text;

namespace Pathfinding.App.Console.Serialization
{
    internal sealed class BinaryHistorySerializer : ISerializer<GraphPathfindingHistory>
    {
        public GraphPathfindingHistory DeserializeFrom(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
            {
                return reader.ReadHistory();
            }
        }

        public void SerializeTo(GraphPathfindingHistory history, Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
            {
                writer.WriteHistory(history);
            }
        }
    }
}
