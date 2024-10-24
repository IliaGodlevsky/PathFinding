using Pathfinding.Service.Interface.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public record CoordinateModel : IBinarySerializable
    {
        public IReadOnlyCollection<int> Coordinate { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Coordinate = reader.ReadArray();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Coordinate);
        }
    }
}
