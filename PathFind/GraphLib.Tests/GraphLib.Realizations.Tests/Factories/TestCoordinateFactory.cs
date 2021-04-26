using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Tests.Objects;

namespace GraphLib.Realizations.Tests.Factories
{
    internal class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(int[] coordinates)
        {
            return new TestCoordinate(coordinates);
        }
    }
}
