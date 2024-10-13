using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class GraphStateSerializationModel : IBinarySerializable
    {
        public IReadOnlyCollection<CoordinateModel> Regulars { get; set; }

        public IReadOnlyCollection<CoordinateModel> Obstacles { get; set; }

        public IReadOnlyCollection<CoordinateModel> Range { get; set; }

        public IReadOnlyCollection<CostCoordinatePair> Costs { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Regulars = reader.ReadSerializableArray<CoordinateModel>();
            Obstacles = reader.ReadSerializableArray<CoordinateModel>();
            Range = reader.ReadSerializableArray<CoordinateModel>();
            Costs = reader.ReadSerializableArray<CostCoordinatePair>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Regulars);
            writer.Write(Obstacles);
            writer.Write(Range);
            writer.Write(Costs);
        }
    }
}
