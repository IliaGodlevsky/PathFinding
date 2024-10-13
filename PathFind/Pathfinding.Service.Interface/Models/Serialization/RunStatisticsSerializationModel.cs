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

        public TimeSpan? Elapsed { get; set; } = null;

        public int? Steps { get; set; } = null;

        public double? Cost { get; set; } = null;

        public string Spread { get; set; } = null;

        public int? Visited { get; set; } = null;

        public void Deserialize(BinaryReader reader)
        {
            AlgorithmRunId = reader.ReadInt32();
            Heuristics = reader.ReadNullableString();
            StepRule = reader.ReadNullableString();
            ResultStatus = reader.ReadString();
            Elapsed = reader.ReadNullableTimeSpan();
            Steps = reader.ReadNullableInt();
            Cost = reader.ReadNullableDouble();
            Spread = reader.ReadNullableString();
            Visited = reader.ReadNullableInt();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(AlgorithmRunId);
            writer.WriteNullableString(Heuristics);
            writer.WriteNullableString(StepRule);
            writer.Write(ResultStatus);
            writer.WriteNullableTimeSpan(Elapsed);
            writer.WriteNullableInt(Steps);
            writer.WriteNullableDouble(Cost);
            writer.WriteNullableString(Spread);
            writer.WriteNullableInt(Visited);
        }
    }
}
