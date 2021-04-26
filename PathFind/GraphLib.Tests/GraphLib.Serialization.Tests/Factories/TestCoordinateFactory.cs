using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Tests.Objects;

namespace GraphLib.Serialization.Tests.Factories
{
    internal class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(int[] coordinates)
        {
            return new TestCoordinate(coordinates);
        }
    }
}
