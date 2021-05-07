using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Plugins.BaseAlgorithmUnitTest.Objects.TestObjects;

namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories
{
    internal sealed class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(int[] coordinates)
        {
            return new TestCoordinate(coordinates);
        }
    }
}
