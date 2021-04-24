using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Common.NullObjects
{
    public sealed class DefaultCoordinateRadar : ICoordinateRadar
    {
        public IEnumerable<int[]> Environment => new List<int[]>();
    }
}