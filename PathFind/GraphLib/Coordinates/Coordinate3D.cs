using GraphLib.Coordinates.Abstractions;
using GraphLib.Coordinates.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Coordinates
{
    [Serializable]
    public sealed class Coordinate3D : BaseCoordinate
    {
        public int X => CoordinatesValues.First();

        public int Y => CoordinatesValues.ElementAt(1);

        public int Z => CoordinatesValues.Last();

        public override IEnumerable<ICoordinate> Environment
        {
            get
            {
                if (coordinateEnvironment == null)
                {
                    var environment = new CoordinateEnvironment<Coordinate3D>(this);
                    coordinateEnvironment = environment.GetEnvironment();
                }

                return coordinateEnvironment;
            }
        }

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
