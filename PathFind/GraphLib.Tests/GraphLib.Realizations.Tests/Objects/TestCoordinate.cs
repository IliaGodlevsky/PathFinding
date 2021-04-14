using GraphLib.Base;

namespace GraphLib.Realizations.Tests.Objects
{
    internal class TestCoordinate : BaseCoordinate
    {
        public TestCoordinate(params int[] coordinates)
            : base(coordinates.Length, coordinates)
        {
        }
    }
}
