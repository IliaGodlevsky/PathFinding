﻿using GraphLib.Base;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GraphLib.Realizations.Coordinates
{
    [DebuggerDisplay("{ToString()}")]
    public sealed class Coordinate2D : BaseCoordinate
    {
        public int X { get; }

        public int Y { get; }

        public Coordinate2D(int x, int y)
            : this(new[] { x, y })
        {

        }

        public Coordinate2D(IReadOnlyList<int> coordinates)
            : base(numberOfDimensions: 2, coordinates)
        {
            X = this.First();
            Y = this.Last();
        }
    }
}
