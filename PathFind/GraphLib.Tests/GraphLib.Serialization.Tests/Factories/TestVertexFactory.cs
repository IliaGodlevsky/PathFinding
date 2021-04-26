using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Tests.Objects;

namespace GraphLib.Serialization.Tests.Factories
{
    internal class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex(ICoordinateRadar coordinateRadar, ICoordinate coordinate)
        {
            return new TestVertex(coordinateRadar, coordinate);
        }
    }
}
