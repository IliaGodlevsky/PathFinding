using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Undefined;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class VertexSerializationModel : IBinarySerializable, IXmlSerializable
    {
        public CoordinateModel Position { get; set; }

        public VertexCostModel Cost { get; set; }

        public bool IsObstacle { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Position = reader.ReadSerializable<CoordinateModel>();
            Cost = reader.ReadSerializable<VertexCostModel>();
            IsObstacle = reader.ReadBoolean();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Position);
            writer.Write(Cost);
            writer.Write(IsObstacle);
        }

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            Position = new CoordinateModel();
            Position.ReadXml(reader);
            Cost = new VertexCostModel();
            Cost.ReadXml(reader);
            IsObstacle = reader.ReadElement<bool>(nameof(IsObstacle));
        }

        public void WriteXml(XmlWriter writer)
        {
            Position.WriteXml(writer);
            Cost.WriteXml(writer);
            writer.WriteElement(nameof(IsObstacle), IsObstacle);
        }
    }
}
