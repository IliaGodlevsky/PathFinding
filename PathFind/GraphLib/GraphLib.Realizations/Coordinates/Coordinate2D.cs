using GraphLib.Base;
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

        public Coordinate2D(params int[] coordinates)
            : base(numberOfDimensions: 2, coordinates)
        {
            X = CoordinatesValues.First();
            Y = CoordinatesValues.Last();
        }
    }
}
