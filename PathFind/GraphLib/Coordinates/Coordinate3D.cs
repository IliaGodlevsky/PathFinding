using GraphLib.Coordinates.Interface;
using GraphLib.Extensions;
using System;
using System.Collections.Generic;

namespace GraphLib.Coordinates
{
    [Serializable]
    public class Coordinate3D : ICoordinate
    {
        public Coordinate3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }


        public IEnumerable<int> Coordinates => new int[] { X, Y, Z };

        public IEnumerable<ICoordinate> Environment
        {
            get
            {
                for (int x = X - 1; x <= X + 1; x++)
                {
                    for (int y = Y - 1; y <= Y + 1; y++)
                    {
                        for (int z = Z - 1; z <= Z + 1; z++)
                        {
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

        public bool IsDefault => false;

        public override bool Equals(object pos)
        {
            ICoordinate coordinate = pos as Coordinate3D;

            if (coordinate == null)
            {
                throw new ArgumentException("Invalid value to compare");
            }

            return this.IsEqual(coordinate);
        }

        public override int GetHashCode()
        {
            return X ^ Y ^ Z;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", X, Y, Z);
        }

        public object Clone()
        {
            return new Coordinate3D(X, Y, Z);
        }
    }
}
