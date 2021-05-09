﻿using Common.Attributes;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Common.NullObjects
{
    [Null]
    public sealed class NullCoordinateRadar : ICoordinateRadar
    {
        public IEnumerable<int[]> Environment => new List<int[]>();
    }
}