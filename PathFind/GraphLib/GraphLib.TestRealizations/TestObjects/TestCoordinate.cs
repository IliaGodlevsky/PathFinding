using GraphLib.Base;
using System.Collections.Generic;

namespace GraphLib.TestRealizations.TestObjects
{
    public sealed class TestCoordinate : BaseCoordinate
    {
        public TestCoordinate(IReadOnlyList<int> coordinates)
            : base(coordinates.Count, coordinates)
        {

        }

        public TestCoordinate(params int[] coordinates)
            : this((IReadOnlyList<int>)coordinates)
        {

        }
    }
}