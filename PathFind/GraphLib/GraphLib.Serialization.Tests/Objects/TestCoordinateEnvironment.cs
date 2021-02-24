using GraphLib.Interface;
using System.Collections.Generic;

namespace GraphLib.Serialization.Tests.Objects
{
    internal class TestCoordinateRadar : ICoordinateRadar
    {
        public IEnumerable<int[]> Environment => new int[][] { };
    }
}
