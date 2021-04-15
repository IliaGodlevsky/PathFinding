using GraphLib.Interfaces;
using GraphLib.Serialization.Tests.Objects;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Serialization.Tests.Factories
{
    internal class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IEnumerable<int> coordinates)
        {
            return new TestCoordinate(coordinates.ToArray());
        }
    }
}
