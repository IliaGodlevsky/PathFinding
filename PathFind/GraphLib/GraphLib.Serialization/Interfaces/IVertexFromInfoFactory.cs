using GraphLib.Interfaces;

namespace GraphLib.Serialization.Interfaces
{
    public interface IVertexFromInfoFactory<out TVertex>
        where TVertex : IVertex
    {
        TVertex CreateFrom(VertexSerializationInfo info);
    }
}
