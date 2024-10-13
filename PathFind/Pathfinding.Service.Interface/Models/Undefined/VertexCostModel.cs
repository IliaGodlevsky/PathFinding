using System.IO;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public class VertexCostModel : IBinarySerializable
    {
        public int Cost { get; set; }

        public int UpperValueOfRange { get; set; }

        public int LowerValueOfRange { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            Cost = reader.ReadInt32();
            UpperValueOfRange = reader.ReadInt32();
            LowerValueOfRange = reader.ReadInt32();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Cost);
            writer.Write(UpperValueOfRange);
            writer.Write(LowerValueOfRange);
        }
    }
}
