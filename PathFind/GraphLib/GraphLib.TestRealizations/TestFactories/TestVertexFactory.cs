using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.TestRealizations.TestObjects;

namespace GraphLib.TestRealizations.TestFactories
{
    public class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex(INeighborhood coordinateRadar, ICoordinate coordinate)
        {
            return new TestVertex(coordinateRadar, coordinate);
        }
    }
}
