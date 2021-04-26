using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;

namespace ConsoleVersion.Model
{
    internal sealed class VertexSerializationInfoConverter : IVertexSerializationInfoConverter
    {
        public VertexSerializationInfoConverter(ICoordinateRadarFactory factory)
        {
            radarFactory = factory;
        }

        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            var radar = radarFactory.CreateCoordinateRadar(info.Position);
            return new Vertex(info, radar);
        }

        private readonly ICoordinateRadarFactory radarFactory;
    }
}