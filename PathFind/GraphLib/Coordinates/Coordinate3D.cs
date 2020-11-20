using GraphLib.Coordinates.Abstractions;
using System;
using System.Collections.Generic;
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

        public override IEnumerable<ICoordinate> Environment
        {
            get
            {
                for (int x = X - 1; x <= X + 1; x++)
                {
                    for (int y = Y - 1; y <= Y + 1; y++)
                    {
                        for (int z = Z - 1; z <= Z + 1; z++)
                        {
                            if (x < 0 || y < 0 || z < 0)
                                continue;

                            var coordinate = new Coordinate3D(x, y, z);

                            if (!Equals(coordinate))
                            {
                                yield return coordinate;
                            }
                        }
                    }
                }
            }
        }

        public override object Clone()
        {
            return new Coordinate3D(Coordinates.ToArray());
        }
    }
}
