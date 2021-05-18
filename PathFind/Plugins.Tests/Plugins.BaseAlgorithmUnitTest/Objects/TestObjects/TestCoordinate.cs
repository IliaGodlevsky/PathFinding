using GraphLib.Base;
using System.Linq;

namespace Plugins.BaseAlgorithmUnitTest.Objects.TestObjects
{
    internal sealed class TestCoordinate : BaseCoordinate
    {
        public int X => CoordinatesValues.First();

        public int Y => CoordinatesValues.Last();

        public TestCoordinate(params int[] coordinates) :
            base(numberOfDimensions: 2, coordinates)
        {

        }
    }
}
