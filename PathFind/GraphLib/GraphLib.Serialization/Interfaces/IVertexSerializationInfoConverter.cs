using GraphLib.Interface;

namespace GraphLib.Serialization.Interfaces
{
    public interface IVertexSerializationInfoConverter
    {
        IVertex ConvertFrom(VertexSerializationInfo info);
    }
}
