using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class PathfindingHistorySerializationModel : IBinarySerializable
    {
        public GraphSerializationModel Graph { get; set; }

        public IReadOnlyCollection<AlgorithmRunHistorySerializationModel> Algorithms { get; set; }

        public IReadOnlyCollection<CoordinateModel> Range { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Graph = reader.ReadSerializable<GraphSerializationModel>();
            Algorithms = reader.ReadSerializableArray<AlgorithmRunHistorySerializationModel>();
            Range = reader.ReadSerializableArray<CoordinateModel>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Graph);
            writer.Write(Algorithms);
            writer.Write(Range);
        }
    }
}
