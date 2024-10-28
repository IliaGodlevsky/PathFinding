using Pathfinding.Service.Interface.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class AlgorithmRunHistorySerializationModel : IBinarySerializable
    {
        public AlgorithmRunSerializationModel Run { get; set; }

        public RunStatisticsSerializationModel Statistics { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Run = reader.ReadSerializable<AlgorithmRunSerializationModel>();
            Statistics = reader.ReadSerializable<RunStatisticsSerializationModel>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Run);
            writer.Write(Statistics);
        }
    }
}
