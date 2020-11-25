using GraphLib.Coordinates.Abstractions;
using System;
using System.Linq;

namespace GraphLib.Coordinates
{
    /// <summary>
    /// Cartesian coordinates of the vertex on the graph
    /// </summary>
    [Serializable]
    public sealed class Coordinate2D : BaseCoordinate
    {
        public int X => Coordinates.First();

        public int Y => Coordinates.Last();

        public Coordinate2D(int x, int y)
            : this(new int[] { x, y })
        {

        }

        public Coordinate2D(params int[] coordinates)
            : base(coordinates)
        {
            if (coordinates.Length != 2) 
            {
                throw new ArgumentException("Must be two coordinates");
            }
        }

        public override object Clone()
        {
            return new Coordinate2D(X, Y);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
