using GraphLib.Base;

namespace Plugins.BaseAlgorithmUnitTest.Objects.TestObjects
{
    internal sealed class TestCoordinate : BaseCoordinate
    {
        public TestCoordinate(params int[] coordinates) :
            base(coordinates.Length, coordinates)
        {

        }
    }
}
