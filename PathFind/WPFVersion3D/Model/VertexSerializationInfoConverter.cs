using GraphLib.Graphs.Serialization.Factories.Interfaces;
using GraphLib.Info;
using GraphLib.Vertex.Interface;

namespace WPFVersion3D.Model
{
    internal class Vertex3DSerializationInfoConverter : IVertexSerializationInfoConverter
    {
        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            return new Vertex3D(info);
        }
    }
}
