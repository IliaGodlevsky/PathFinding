using GraphLib.Base;

namespace Algorithm.Realizations.Tests.Objects
{
    internal sealed class TestCoordinate : BaseCoordinate
    {
        public TestCoordinate(params int[] coordinates) :
            base(coordinates.Length, coordinates)
        {

        }
    }
}
