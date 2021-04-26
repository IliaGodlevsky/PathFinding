using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;

namespace GraphLib.Serialization.Tests.Objects
{
    public class TestVertexInfoSerializationConverter : IVertexSerializationInfoConverter
    {
        public TestVertexInfoSerializationConverter(ICoordinateRadarFactory factory)
        {
            this.factory = factory;
        }

        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            var radar = factory.CreateCoordinateRadar(info.Position);
            return new TestVertex(info, radar);
        }

        private readonly ICoordinateRadarFactory factory;
    }
}
