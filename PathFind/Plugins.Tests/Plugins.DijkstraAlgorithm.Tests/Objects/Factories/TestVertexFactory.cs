using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Plugins.DijkstraAlgorithm.Tests.Objects.TestObjects;

namespace Plugins.DijkstraAlgorithm.Tests.Objects.Factories
{
    internal sealed class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex(ICoordinateRadar coordinateRadar, ICoordinate coordinate)
        {
            return new TestVertex(coordinateRadar, coordinate);
        }
    }
}
