using GraphLib.Base;
using System;

namespace GraphLib.TestRealizations.TestObjects
{
    [Serializable]
    public sealed class TestCoordinate : BaseCoordinate
    {
        public TestCoordinate(params int[] coordinates) :
            base(coordinates.Length, coordinates)
        {

        }
    }
}
