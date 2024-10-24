using Pathfinding.Service.Interface.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public record GraphSerializationModel : IBinarySerializable
    {
        public string Name { get; set; }

        public string SmoothLevel { get; set; }

        public string Neighborhood { get; set; }

        public IReadOnlyList<int> DimensionSizes { get; set; }

        public IReadOnlyCollection<VertexSerializationModel> Vertices { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Name = reader.ReadString();
            SmoothLevel = reader.ReadString();
            Neighborhood = reader.ReadString();
            DimensionSizes = reader.ReadArray();
            Vertices = reader.ReadSerializableArray<VertexSerializationModel>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(SmoothLevel);
            writer.Write(Neighborhood);
            writer.Write(DimensionSizes);
            writer.Write(Vertices);
        }
    }
}
