using GraphLib.Base;
using System;

namespace GraphLib.Serialization.Tests.Objects
{
    [Serializable]
    internal class TestCoordinate : BaseCoordinate
    {
        public TestCoordinate(params int[] coordinates)
            : base(coordinates.Length, coordinates)
        {
        }
    }
}
