using GraphLib.Base;
using System;

namespace GraphLib.Tests.TestInfrastructure
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
