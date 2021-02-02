using GraphLib.Infrastructure;
using GraphLib.Interface;

namespace WPFVersion.Model
{
    internal class VertexSerializationInfoConverter : IVertexSerializationInfoConverter
    {
        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            return new Vertex(info);
        }
    }
}
