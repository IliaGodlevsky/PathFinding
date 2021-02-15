using Algorithm.Realizations.Tests.Objects;
using GraphLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.Tests.Factories
{
    public sealed class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IEnumerable<int> coordinates)
        {
            return new TestCoordinate(coordinates.ToArray());
        }
    }
}
