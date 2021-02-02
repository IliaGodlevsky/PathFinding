using GraphLib.Infrastructure;

namespace GraphLib.Interface
{
    public interface IVertexSerializationInfoConverter
    {
        IVertex ConvertFrom(VertexSerializationInfo info);
    }
}
