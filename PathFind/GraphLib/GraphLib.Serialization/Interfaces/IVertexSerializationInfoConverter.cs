using GraphLib.Interfaces;

namespace GraphLib.Serialization.Interfaces
{
    public interface IVertexSerializationInfoConverter
    {
        IVertex ConvertFrom(VertexSerializationInfo info);
    }
}
