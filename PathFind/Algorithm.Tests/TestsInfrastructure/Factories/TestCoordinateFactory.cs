using Algorithm.Tests.TestsInfrastructure.Objects;
using GraphLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Tests.TestsInfrastructure.Factories
{
    public sealed class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IEnumerable<int> coordinates)
        {
            return new TestCoordinate(coordinates.ToArray());
        }
    }
}
