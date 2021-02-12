using GraphLib.Base;
using GraphLib.Interface;

namespace Algorithm.Tests.TestsInfrastructure.Objects
{
    internal sealed class TestCoordinate : BaseCoordinate
    {
        public TestCoordinate(params int[] coordinates) :
            base(coordinates.Length, coordinates)
        {

        }

        protected override ICoordinate CreateInstance(int[] values)
        {
            return new TestCoordinate(values);
        }
    }
}
