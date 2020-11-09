using GraphLib.Coordinates.Interface;
using GraphLib.Extensions;
using System;
using System.Collections.Generic;

namespace GraphLib.Coordinates
{
    /// <summary>
    /// Cartesian coordinates of the vertex on the graph
    /// </summary>
    [Serializable]
    public class Coordinate2D : ICoordinate
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public IEnumerable<int> Coordinates => new int[] { X, Y };

        public IEnumerable<ICoordinate> Environment
        {
            get
            {
                for (int x = X - 1; x <= X + 1; x++)
                {
                    for (int y = Y - 1; y <= Y + 1; y++)
                    {
                        var coordinate = new Coordinate2D(x, y);

                        if (!Equals(coordinate))
                        {
                            yield return coordinate;
                        }
                    }
                }
            }
        }

        public bool IsDefault => false;

        public Coordinate2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object pos)
        {
            if (pos == null)
                return false;

            return this.IsEqual(pos as ICoordinate);
        }

        public static bool operator ==(Coordinate2D position1, Coordinate2D position2)
        {
            return position1.Equals(position2);
        }

        public static bool operator !=(Coordinate2D position1, Coordinate2D position2)
        {
            return !position1.Equals(position2);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", X, Y);
        }

        public object Clone()
        {
            return new Coordinate2D(X, Y);
        }
    }
}
