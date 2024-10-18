using Pathfinding.Service.Interface.Extensions;
using System;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class RunStatisticsSerializationModel : IBinarySerializable
    {
        public int AlgorithmRunId { get; set; }

        public string Heuristics { get; set; } = null;

        public string StepRule { get; set; } = null;

        public string ResultStatus { get; set; } = string.Empty;

        public TimeSpan Elapsed { get; set; }

        public int Steps { get; set; }

        public double Cost { get; set; }

        public string Spread { get; set; } = null;

        public int Visited { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            AlgorithmRunId = reader.ReadInt32();
            Heuristics = reader.ReadNullableString();
            StepRule = reader.ReadNullableString();
            ResultStatus = reader.ReadString();
            Elapsed = TimeSpan.FromMilliseconds(reader.ReadDouble());
            Steps = reader.ReadInt32();
            Cost = reader.ReadDouble();
            Spread = reader.ReadNullableString();
            Visited = reader.ReadInt32();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(AlgorithmRunId);
            writer.WriteNullableString(Heuristics);
            writer.WriteNullableString(StepRule);
            writer.Write(ResultStatus);
            writer.Write(Elapsed.TotalMilliseconds);
            writer.Write(Steps);
            writer.Write(Cost);
            writer.WriteNullableString(Spread);
            writer.Write(Visited);
        }
    }
}
