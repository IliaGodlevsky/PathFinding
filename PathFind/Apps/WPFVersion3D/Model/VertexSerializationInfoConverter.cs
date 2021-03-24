using GraphLib.Interface;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DSerializationInfoConverter : IVertexSerializationInfoConverter
    {
        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            return new Vertex3D(info);
        }
    }
}
