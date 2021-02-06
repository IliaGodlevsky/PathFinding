using GraphLib.Base;
using GraphLib.Interface;
using System;
using System.Linq;

namespace GraphLib.NullObjects
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

        protected override ICoordinate CreateInstance(int[] values)
        {
            return new Coordinate2D(values);
        }
    }
}
