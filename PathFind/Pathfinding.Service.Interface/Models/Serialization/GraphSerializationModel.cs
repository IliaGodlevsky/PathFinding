using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public record GraphSerializationModel : IBinarySerializable
    {
        public string Name { get; set; }

        public SmoothLevels SmoothLevel { get; set; }

        public Neighborhoods Neighborhood { get; set; }

        public GraphStatuses Status { get; set; }

        public IReadOnlyList<int> DimensionSizes { get; set; }

        public IReadOnlyCollection<VertexSerializationModel> Vertices { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Name = reader.ReadString();
            SmoothLevel = (SmoothLevels)reader.ReadInt32();
            Neighborhood = (Neighborhoods)reader.ReadInt32();
            Status = (GraphStatuses)reader.ReadInt32();
            DimensionSizes = reader.ReadArray();
            Vertices = reader.ReadSerializableArray<VertexSerializationModel>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write((int)SmoothLevel);
            writer.Write((int)Neighborhood);
            writer.Write((int)Status);
            writer.Write(DimensionSizes);
            writer.Write(Vertices);
        }
    }
}
