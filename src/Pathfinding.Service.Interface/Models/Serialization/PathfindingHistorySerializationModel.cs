using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class PathfindingHistorySerializationModel : IBinarySerializable, IXmlSerializable
    {
        public GraphSerializationModel Graph { get; set; }

        public IReadOnlyCollection<RunStatisticsSerializationModel> Statistics { get; set; }

        public IReadOnlyCollection<CoordinateModel> Range { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Graph = reader.ReadSerializable<GraphSerializationModel>();
            Statistics = reader.ReadSerializableArray<RunStatisticsSerializationModel>();
            Range = reader.ReadSerializableArray<CoordinateModel>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Graph);
            writer.Write(Statistics);
            writer.Write(Range);
        }

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            Graph = new GraphSerializationModel();
            Graph.ReadXml(reader);
            Statistics = reader.ReadCollection<RunStatisticsSerializationModel>(nameof(Statistics), "Statistic");
            Range = reader.ReadCollection<CoordinateModel>(nameof(Range), "Coordinates");
        }

        public void WriteXml(XmlWriter writer)
        {
            Graph.WriteXml(writer);
            writer.WriteCollection(nameof(Statistics), "Statistic", Statistics);
            writer.WriteCollection(nameof(Range), "Coordinates", Range);
        }
    }
}
