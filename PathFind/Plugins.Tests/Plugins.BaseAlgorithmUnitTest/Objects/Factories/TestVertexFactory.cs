using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Plugins.BaseAlgorithmUnitTest.Objects.TestObjects;

namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories
{
    internal sealed class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex(ICoordinateRadar coordinateRadar, ICoordinate coordinate)
        {
            return new TestVertex(coordinateRadar, coordinate);
        }
    }
}
