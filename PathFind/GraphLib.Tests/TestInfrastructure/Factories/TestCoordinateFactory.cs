using GraphLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Tests.TestInfrastructure.Factories
{
    internal class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IEnumerable<int> coordinates)
        {
            return new TestCoordinate(coordinates.ToArray());
        }
    }
}
