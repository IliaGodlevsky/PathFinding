using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestFactories
{
    public class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IReadOnlyList<int> coordinates)
        {
            return new TestCoordinate(coordinates);
        }
    }
}
