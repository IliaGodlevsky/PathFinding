using System.IO;

namespace Pathfinding.Service.Interface
{
    public interface IBinarySerializable
    {
        void Serialize(BinaryWriter writer);

        void Deserialize(BinaryReader reader);
    }
}
