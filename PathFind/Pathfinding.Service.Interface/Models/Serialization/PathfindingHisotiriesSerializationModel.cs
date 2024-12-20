using Pathfinding.Service.Interface.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class PathfindingHisotiriesSerializationModel : IBinarySerializable
    {
        public IReadOnlyCollection<PathfindingHistorySerializationModel> Histories { get; set; }
            = Array.Empty<PathfindingHistorySerializationModel>();

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
