using GraphLib.Base;
using GraphLib.Interface;
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

        protected override ICoordinate CreateInstance(int[] values)
        {
            return new TestCoordinate(values);
        }
    }
}
