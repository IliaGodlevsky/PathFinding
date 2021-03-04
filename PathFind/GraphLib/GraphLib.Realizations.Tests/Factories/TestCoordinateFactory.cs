using GraphLib.Interface;
using GraphLib.Realizations.Tests.Objects;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Tests.Factories
{
    internal class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IEnumerable<int> coordinates)
        {
            return new TestCoordinate(coordinates.ToArray());
        }
    }
}
