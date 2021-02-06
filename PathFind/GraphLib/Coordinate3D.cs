using GraphLib.Base;
using GraphLib.Interface;
using System;
using System.Linq;

namespace GraphLib.NullObjects
{
    /// <summary>
    ///  A class representing cartesian three-dimensional coordinates
    /// </summary>
    [Serializable]
    public sealed class Coordinate3D : BaseCoordinate
    {
        public int X => CoordinatesValues.First();

        public int Y => CoordinatesValues.ElementAt(1);

        public int Z => CoordinatesValues.Last();

        public Coordinate3D(params int[] coordinates)
            : base(numberOfDimensions: 3, coordinates)
        {

        }

        public Coordinate3D(int x, int y, int z)
            : this(new int[] { x, y, z })
        {

        }

        protected override ICoordinate CreateInstance(int[] values)
        {
            return new Coordinate3D(values);
        }
    }
}
