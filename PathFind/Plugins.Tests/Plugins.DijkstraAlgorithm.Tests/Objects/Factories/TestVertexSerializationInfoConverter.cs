using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using Plugins.DijkstraAlgorithm.Tests.Objects.TestObjects;

namespace Plugins.DijkstraAlgorithm.Tests.Objects.Factories
{
    internal sealed class TestVertexSerializationInfoConverter : IVertexSerializationInfoConverter
    {
        public TestVertexSerializationInfoConverter(ICoordinateRadarFactory radarFactory)
        {
            this.radarFactory = radarFactory;
        }
        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            return new TestVertex(info, radarFactory.CreateCoordinateRadar(info.Position));
        }

        private readonly ICoordinateRadarFactory radarFactory;
    }
}
