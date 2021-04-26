using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Tests.Objects;

namespace GraphLib.Realizations.Tests.Factories
{
    internal class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex(ICoordinateRadar coordinateRadar, ICoordinate coordinate)
        {
            return new TestVertex(coordinateRadar, coordinate);
        }
    }
}
