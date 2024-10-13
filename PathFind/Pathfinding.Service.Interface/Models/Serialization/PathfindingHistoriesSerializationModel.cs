using Pathfinding.Service.Interface.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class PathfindingHistoriesSerializationModel : IBinarySerializable
    {
        public IReadOnlyCollection<PathfindingHistorySerializationModel> Histories { get; set; }
            = new List<PathfindingHistorySerializationModel>();

        public void Deserialize(BinaryReader reader)
        {
            Histories = reader.ReadSerializableArray<PathfindingHistorySerializationModel>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Histories);
        }
    }
}
