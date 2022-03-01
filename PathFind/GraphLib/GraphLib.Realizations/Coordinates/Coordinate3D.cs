using Common.Interface;
using GraphLib.Base;
using GraphLib.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;

namespace GraphLib.Realizations.Coordinates
{
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    public sealed class Coordinate3D : BaseCoordinate, ICoordinate, ICloneable<ICoordinate>
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

        public override ICoordinate Clone()
        {
            return new Coordinate3D(X, Y, Z);
        }
    }
}
