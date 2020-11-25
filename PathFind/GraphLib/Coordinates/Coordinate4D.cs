using GraphLib.Coordinates.Abstractions;
using System;
using System.Linq;

namespace GraphLib.Coordinates
{
    [Serializable]
    public sealed class Coordinate4D : BaseCoordinate
    {
        public int X => Coordinates.First();

        public int Y => Coordinates.ElementAt(1);

        public int Z => Coordinates.ElementAt(2);

        public int W => Coordinates.Last();

        public Coordinate4D(params int[] coordinates)
            : base(coordinates)
        {
            if (coordinates.Length != 4)
            {
                throw new ArgumentException("Must be four coordinates");
            }
        }

        public Coordinate4D(int x, int y, int z, int w)
            : this(new int[] { x, y, z, w })
        {

        }

        public override object Clone()
        {
            return new Coordinate4D(X, Y, Z, W);
        }
    }
}
