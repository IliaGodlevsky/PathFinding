using Pathfinding.Service.Interface.Extensions;
using System;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class RunStatisticsSerializationModel : IBinarySerializable
    {
        public string AlgorithmName { get; set; }

        public string Heuristics { get; set; } = null;

        public double? Weight { get; set; }

        public string StepRule { get; set; } = null;

        public string ResultStatus { get; set; } = string.Empty;

        public TimeSpan Elapsed { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }

        public int Visited { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            AlgorithmName = reader.ReadString();
            Heuristics = reader.ReadNullableString();
            Weight = reader.ReadNullableDouble();
            StepRule = reader.ReadNullableString();
            ResultStatus = reader.ReadString();
            Elapsed = TimeSpan.FromMilliseconds(reader.ReadDouble());
            Steps = reader.ReadInt32();
            Cost = reader.ReadDouble();
            Visited = reader.ReadInt32();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(AlgorithmName);
            writer.WriteNullableString(Heuristics);
            writer.Write(Weight);
            writer.WriteNullableString(StepRule);
            writer.Write(ResultStatus);
            writer.Write(Elapsed.TotalMilliseconds);
            writer.Write(Steps);
            writer.Write(Cost);
            writer.Write(Visited);
        }
    }
}
