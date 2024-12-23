using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public record GraphSerializationModel : IBinarySerializable, IXmlSerializable
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

        public XmlSchema GetSchema() => null;

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttribute("Name", Name);
            writer.WriteAttribute("SmoothLevel", SmoothLevel);
            writer.WriteAttribute("Neighborhood", Neighborhood);
            writer.WriteAttribute("Status", Status);
            writer.WriteAttribute("DimensionSizes", string.Join(",", DimensionSizes));
            writer.WriteCollection("Vertices", "Vertex", Vertices);
        }

        public void ReadXml(XmlReader reader)
        {
            Name = reader.ReadAttribute<string>("Name");
            SmoothLevel = (SmoothLevels)Enum.Parse(typeof(SmoothLevels), reader.ReadAttribute<string>("SmoothLevel"));
            Neighborhood = (Neighborhoods)Enum.Parse(typeof(Neighborhoods), reader.ReadAttribute<string>("Neighborhood"));
            Status = (GraphStatuses)Enum.Parse(typeof(GraphStatuses), reader.ReadAttribute<string>("Status"));
            DimensionSizes = Array.ConvertAll(reader.ReadAttribute<string>("DimensionSizes").Split(','), int.Parse);
            reader.Read();
            Vertices = reader.ReadCollection<VertexSerializationModel>("Vertices", "Vertex");
        }
    }
}
