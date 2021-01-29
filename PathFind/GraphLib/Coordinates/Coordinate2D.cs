using GraphLib.Coordinates.Abstractions;
using System;
using System.Linq;

namespace GraphLib.Coordinates
{
    /// <summary>
    /// A class representing cartesian two-dimensional coordinates
    /// </summary>
    [Serializable]
    public sealed class Coordinate2D : BaseCoordinate
    {
        public int X => CoordinatesValues.First();

        public int Y => CoordinatesValues.Last();

        public Coordinate2D(int x, int y)
            : this(new int[] { x, y })
        {

        }

        public Coordinate2D(params int[] coordinates)
            : base(numberOfDimensions: 2, coordinates)
        {

        }

        public override object Clone()
        {
            return new Coordinate2D(X, Y);
        }

        protected override ICoordinate CreateInstance(int[] values)
        {
            return new Coordinate2D(values);
        }
    }
}
