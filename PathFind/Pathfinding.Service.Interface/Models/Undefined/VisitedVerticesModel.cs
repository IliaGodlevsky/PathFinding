using System.Collections.Generic;
using System.IO;
using Pathfinding.Service.Interface.Extensions;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public class VisitedVerticesModel : IBinarySerializable
    {
        public CoordinateModel Current { get; set; }

        public IReadOnlyCollection<CoordinateModel> Enqueued { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Current = reader.ReadSerializable<CoordinateModel>();
            Enqueued = reader.ReadSerializableArray<CoordinateModel>();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Current);
            writer.Write(Enqueued);
        }
    }
}
