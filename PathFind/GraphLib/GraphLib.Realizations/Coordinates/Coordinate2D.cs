using GraphLib.Base;
using System;
using System.Linq;

namespace GraphLib.Realizations.Coordinates
{
    /// <summary>
    /// A class representing cartesian two-dimensional coordinates
    /// </summary>
    [Serializable]
    public sealed class Coordinate2D : BaseCoordinate
    {
        public int X { get; } 

        public int Y { get; }

        public Coordinate2D(int x, int y)
            : this(new[] { x, y })
        {
            X = CoordinatesValues.First();
            Y = CoordinatesValues.Last();
        }

        public Coordinate2D(params int[] coordinates)
            : base(numberOfDimensions: 2, coordinates)
        {

        }
    }
}
