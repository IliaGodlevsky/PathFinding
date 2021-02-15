using GraphLib.Interface;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;

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
