using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class SubAlgorithmSerializationModel : IBinarySerializable
    {
        public int Order { get; set; }

        public IReadOnlyCollection<CoordinateModel> Path { get; set; }

        public IReadOnlyCollection<VisitedVerticesModel> Visited { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Order = reader.ReadInt32();
            Path = reader.ReadSerializableArray<CoordinateModel>();
            Visited = reader.ReadSerializableArray<VisitedVerticesModel>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Order);
            writer.Write(Path);
            writer.Write(Visited);
        }
    }
}
