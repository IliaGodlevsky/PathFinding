using Pathfinding.Service.Interface.Extensions;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public class VertexCostModel : IBinarySerializable, IXmlSerializable
    {
        public int Cost { get; set; }

        public int UpperValueOfRange { get; set; }

        public int LowerValueOfRange { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Cost = reader.ReadInt32();
            UpperValueOfRange = reader.ReadInt32();
            LowerValueOfRange = reader.ReadInt32();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Cost);
            writer.Write(UpperValueOfRange);
            writer.Write(LowerValueOfRange);
        }

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            Cost = reader.ReadElement<int>(nameof(Cost));
            UpperValueOfRange = reader.ReadElement<int>(nameof(UpperValueOfRange));
            LowerValueOfRange = reader.ReadElement<int>(nameof(LowerValueOfRange));
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElement(nameof(Cost), Cost);
            writer.WriteElement(nameof(UpperValueOfRange), UpperValueOfRange);
            writer.WriteElement(nameof(LowerValueOfRange), LowerValueOfRange);
        }
    }
}
