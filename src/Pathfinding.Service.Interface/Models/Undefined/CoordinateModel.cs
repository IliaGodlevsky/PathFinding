using Pathfinding.Service.Interface.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public record CoordinateModel : IBinarySerializable, IXmlSerializable
    {
        public IReadOnlyCollection<int> Coordinate { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Coordinate = reader.ReadArray();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Coordinate);
        }

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            var coordinates = reader.ReadElement<string>(nameof(Coordinate))
                                    .Split(',')
                                    .Select(int.Parse)
                                    .ToList();
            Coordinate = coordinates;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElement(nameof(Coordinate), string.Join(",", Coordinate));
        }
    }
}
