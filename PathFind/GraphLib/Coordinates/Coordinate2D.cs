using GraphLib.Coordinates.Abstractions;
using GraphLib.Coordinates.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Coordinates
{
    [Serializable]
    public sealed class Coordinate2D : BaseCoordinate
    {
        public int X => Coordinates.First();

        public int Y => Coordinates.Last();

        public override IEnumerable<ICoordinate> Environment
        {
            get
            {
                if (coordinateEnvironment == null) 
                {
                    var environment = new CoordinateEnvironment<Coordinate2D>(this);
                    coordinateEnvironment = environment.GetEnvironment();
                }

                return coordinateEnvironment;
            }
        }

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
    }
}
