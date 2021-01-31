using GraphLib.Info;
using GraphLib.Vertex.Interface;

namespace GraphLib.Graphs.Serialization.Factories.Interfaces
{
    public interface IVertexSerializationInfoConverter
    {
        IVertex ConvertFrom(VertexSerializationInfo info);
    }
}
