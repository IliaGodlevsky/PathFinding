using GraphLib.Interfaces;

namespace GraphLib.Serialization.Interfaces
{
    public interface IVertexFromInfoFactory
    {
        IVertex CreateFrom(VertexSerializationInfo info);
    }
}
