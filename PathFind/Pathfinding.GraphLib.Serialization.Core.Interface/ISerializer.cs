using System.IO;

namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public interface ISerializer<T>
    {
        void SerializeTo(T item, Stream stream);

        T DeserializeFrom(Stream stream);
    }
}
