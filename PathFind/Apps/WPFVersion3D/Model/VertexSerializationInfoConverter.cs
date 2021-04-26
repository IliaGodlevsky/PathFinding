using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DSerializationInfoConverter : IVertexSerializationInfoConverter
    {
        public Vertex3DSerializationInfoConverter(ICoordinateRadarFactory factory)
        {
            this.factory = factory;
        }

        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            var radar = factory.CreateCoordinateRadar(info.Position);
            return new Vertex3D(info, radar);
        }

        private readonly ICoordinateRadarFactory factory;
    }
}
