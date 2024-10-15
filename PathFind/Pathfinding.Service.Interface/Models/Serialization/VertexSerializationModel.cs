using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class VertexSerializationModel : IBinarySerializable
    {
        public CoordinateModel Position { get; set; }

        public VertexCostModel Cost { get; set; }

        public bool IsObstacle { get; set; }

        public IReadOnlyCollection<CoordinateModel> Neighbors { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Position = reader.ReadSerializable<CoordinateModel>();
            Cost = reader.ReadSerializable<VertexCostModel>();
            IsObstacle = reader.ReadBoolean();
            Neighbors = reader.ReadSerializableArray<CoordinateModel>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Position);
            writer.Write(Cost);
            writer.Write(IsObstacle);
            writer.Write(Neighbors);
        }
    }
}
