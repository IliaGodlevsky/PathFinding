using Pathfinding.GraphLib.Core.Abstractions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Coordinates
{
    [DebuggerDisplay("{ToString()}")]
    public sealed class Coordinate2D : Coordinate
    {
        public static readonly Coordinate2D Empty
            = new Coordinate2D(0, 0);

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
