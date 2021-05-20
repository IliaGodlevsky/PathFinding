using GraphLib.Interfaces;
using NullObject.Attributes;
using System;
using System.Collections.Generic;

namespace GraphLib.Common.NullObjects
{
    [Null]
    [Serializable]
    public sealed class NullCoordinateRadar : ICoordinateRadar
    {
        public IEnumerable<ICoordinate> Environment => new List<NullCoordinate>();
    }
}