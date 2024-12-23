using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Extensions;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class RunStatisticsSerializationModel : IBinarySerializable, IXmlSerializable
    {
        public Domain.Core.Algorithms Algorithm { get; set; }

        public HeuristicFunctions? Heuristics { get; set; } = null;

        public double? Weight { get; set; }

        public StepRules? StepRule { get; set; } = null;

        public RunStatuses ResultStatus { get; set; }

        public TimeSpan Elapsed { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }

        public int Visited { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Algorithm = (Domain.Core.Algorithms)reader.ReadInt32();
            Heuristics = (HeuristicFunctions?)reader.ReadNullableInt32();
            Weight = reader.ReadNullableDouble();
            StepRule = (StepRules?)reader.ReadNullableInt32();
            ResultStatus = (RunStatuses)reader.ReadInt32();
            Elapsed = TimeSpan.FromMilliseconds(reader.ReadDouble());
            Steps = reader.ReadInt32();
            Cost = reader.ReadDouble();
            Visited = reader.ReadInt32();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write((int)Algorithm);
            writer.WriteNullableInt32((int?)Heuristics);
            writer.Write(Weight);
            writer.WriteNullableInt32((int?)StepRule);
            writer.Write((int)ResultStatus);
            writer.Write(Elapsed.TotalMilliseconds);
            writer.Write(Steps);
            writer.Write(Cost);
            writer.Write(Visited);
        }

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            Algorithm = (Domain.Core.Algorithms)Enum.Parse(typeof(Domain.Core.Algorithms), reader.ReadElement<string>("Algorithm"));
            Heuristics = reader.ReadNullableEnum<HeuristicFunctions>("Heuristics");
            Weight = reader.ReadNullableElement<double>("Weight");
            StepRule = reader.ReadNullableEnum<StepRules>("StepRule");
            ResultStatus = (RunStatuses)Enum.Parse(typeof(RunStatuses), reader.ReadElement<string>("ResultStatus"));
            Elapsed = TimeSpan.FromMilliseconds(reader.ReadElement<double>("Elapsed"));
            Steps = reader.ReadElement<int>("Steps");
            Cost = reader.ReadElement<double>("Cost");
            Visited = reader.ReadElement<int>("Visited");
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElement("Algorithm", Algorithm.ToString());
            writer.WriteNullableElement("Heuristics", Heuristics);
            writer.WriteNullableElement("Weight", Weight);
            writer.WriteNullableElement("StepRule", StepRule);
            writer.WriteElement("ResultStatus", ResultStatus.ToString());
            writer.WriteElement("Elapsed", Elapsed.TotalMilliseconds);
            writer.WriteElement("Steps", Steps);
            writer.WriteElement("Cost", Cost);
            writer.WriteElement("Visited", Visited);
        }
    }
}
