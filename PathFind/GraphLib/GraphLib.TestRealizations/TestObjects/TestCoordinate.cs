using GraphLib.Base;
using GraphLib.Interfaces;
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

        public override ICoordinate Clone()
        {
            return new TestCoordinate(CoordinatesValues);
        }
    }
}
