using GraphLib.Coordinates.Abstractions;
using System;
using System.Linq;

namespace GraphLib.Coordinates
{
    [Serializable]
    public sealed class Coordinate3D : BaseCoordinate
    {
        public int X => Coordinates.First();

        public int Y => Coordinates.ElementAt(1);

        public int Z => Coordinates.Last();

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
    }
}
