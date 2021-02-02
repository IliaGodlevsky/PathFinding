using GraphLib.Infrastructure;
using GraphLib.Interface;

namespace WindowsFormsVersion.Model
{
    internal class VertexSerializationInfoConverter : IVertexSerializationInfoConverter
    {
        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            return new Vertex(info);
        }
    }
}
