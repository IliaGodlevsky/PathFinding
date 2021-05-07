using GraphLib.Base;

namespace Plugins.DijkstraAlgorithm.Tests.Objects.TestObjects
{
    internal sealed class TestCoordinate : BaseCoordinate
    {
        public TestCoordinate(params int[] coordinates) : 
            base(coordinates.Length, coordinates)
        {

        }
    }
}
