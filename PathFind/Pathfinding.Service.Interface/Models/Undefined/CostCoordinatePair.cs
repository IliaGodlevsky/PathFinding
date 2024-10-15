using Pathfinding.Service.Interface.Extensions;
using System.IO;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public class CostCoordinatePair : IBinarySerializable
    {
        public CoordinateModel Position { get; set; }

        public int Cost { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Position = reader.ReadSerializable<CoordinateModel>();
            Cost = reader.ReadInt32();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Position);
            writer.Write(Cost);
        }
    }
}
