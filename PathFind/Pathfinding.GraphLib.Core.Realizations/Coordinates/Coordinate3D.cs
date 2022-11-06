using Pathfinding.GraphLib.Core.Abstractions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Coordinates
{
    [DebuggerDisplay("{ToString()}")]
    public sealed class Coordinate3D : Coordinate
    {
        public int X { get; }

        public int Y { get; }

        public int Z { get; }

        public Coordinate3D(IReadOnlyList<int> coordinates)
            : base(numberOfDimensions: 3, coordinates)
        {
            X = this.First();
            Y = this.ElementAt(1);
            Z = this.Last();
        }

        public Coordinate3D(int x, int y, int z)
            : this(new[] { x, y, z })
        {

        }
    }
}
