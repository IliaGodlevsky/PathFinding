using GraphLib.Interfaces;
using NullObject.Attributes;
using System.Collections.Generic;

namespace GraphLib.Common.NullObjects
{
    [Null]
    public sealed class NullCoordinateRadar : ICoordinateRadar
    {
        public IEnumerable<int[]> Environment => new List<int[]>();
    }
}