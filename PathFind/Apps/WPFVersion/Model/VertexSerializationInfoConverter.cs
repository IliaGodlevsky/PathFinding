using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;

namespace WPFVersion.Model
{
    internal sealed class VertexSerializationInfoConverter : IVertexSerializationInfoConverter
    {
        public VertexSerializationInfoConverter(ICoordinateRadarFactory factory)
        {
            this.factory = factory;
        }

        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            var radar = factory.CreateCoordinateRadar(info.Position);
            return new Vertex(info, radar);
        }

        private readonly ICoordinateRadarFactory factory;
    }
}
