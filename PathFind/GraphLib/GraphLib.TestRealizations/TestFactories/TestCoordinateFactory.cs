using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.TestRealizations.TestObjects;
using System.Collections.Generic;

namespace GraphLib.TestRealizations.TestFactories
{
    public class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IReadOnlyList<int> coordinates)
        {
            return new TestCoordinate(coordinates);
        }
    }
}
