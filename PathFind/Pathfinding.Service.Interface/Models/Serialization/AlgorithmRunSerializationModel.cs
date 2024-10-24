using System.IO;

namespace Pathfinding.Service.Interface.Models.Serialization
{
    public class AlgorithmRunSerializationModel : IBinarySerializable
    {
        public string AlgorithmId { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            AlgorithmId = reader.ReadString();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(AlgorithmId);
        }
    }
}
