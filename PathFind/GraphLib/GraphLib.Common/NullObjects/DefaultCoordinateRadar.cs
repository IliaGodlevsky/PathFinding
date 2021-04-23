using System.Collections.Generic;
using GraphLib.Interfaces;

namespace GraphLib.Common.NullObjects
{
    public sealed class DefaultCoordinateRadar : ICoordinateRadar
    {
        public IEnumerable<int[]> Environment => new List<int[]>();
    }
}