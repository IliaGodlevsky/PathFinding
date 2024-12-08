using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Extensions;
using System;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class RunStatisticsSerializationModel : IBinarySerializable
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
    }
}
