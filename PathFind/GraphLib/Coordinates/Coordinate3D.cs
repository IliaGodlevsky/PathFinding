using GraphLib.Coordinates.Abstractions;
using System;
using System.Linq;

namespace GraphLib.Coordinates
{
    /// <summary>
    ///  A class representing cartesian three-dimensional coordinates
    /// </summary>
    [Serializable]
    public sealed class Coordinate3D : BaseCoordinate<Coordinate3D>
    {
        public int X => CoordinatesValues.First();

        public int Y => CoordinatesValues.ElementAt(1);

        public int Z => CoordinatesValues.Last();

        public Coordinate3D(params int[] coordinates)
            : base(coordinates)
        {
            if (coordinates.Length != 3)
            {
                throw new ArgumentException("Must be three coordinates");
            }
        }

        public Coordinate3D(int x, int y, int z)
            : this(new int[] { x, y, z })
        {

        }

        public override object Clone()
        {
            return new Coordinate3D(X, Y, Z);
        }
    }
}
