using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.TestRealizations.TestObjects;

namespace GraphLib.TestRealizations.TestFactories
{
    public class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex(INeighboursCoordinates coordinateRadar, ICoordinate coordinate)
        {
            return new TestVertex(coordinateRadar, coordinate);
        }
    }
}
