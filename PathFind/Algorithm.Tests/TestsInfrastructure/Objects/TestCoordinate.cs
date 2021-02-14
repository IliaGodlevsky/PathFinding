using GraphLib.Base;

namespace Algorithm.Tests.TestsInfrastructure.Objects
{
    internal sealed class TestCoordinate : BaseCoordinate
    {
        public TestCoordinate(params int[] coordinates) :
            base(coordinates.Length, coordinates)
        {

        }
    }
}
