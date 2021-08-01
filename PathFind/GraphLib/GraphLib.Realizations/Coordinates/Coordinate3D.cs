using GraphLib.Base;
using System;
using System.Linq;

namespace GraphLib.Realizations.Coordinates
{
    /// <summary>
    ///  A class representing cartesian three-dimensional coordinates
    /// </summary>
    [Serializable]
    public sealed class Coordinate3D : BaseCoordinate
    {
        public int X { get; }

        public int Y { get; }

        public int Z { get; }

        public Coordinate3D(params int[] coordinates)
            : base(numberOfDimensions: 3, coordinates)
        {
            X = CoordinatesValues.First();
            Y = CoordinatesValues.ElementAt(1);
            Z = CoordinatesValues.Last();
        }

        public Coordinate3D(int x, int y, int z)
            : this(new[] { x, y, z })
        {

        }
    }
}
