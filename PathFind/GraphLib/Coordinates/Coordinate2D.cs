using GraphLib.Coordinates.Abstractions;
using System;
using System.Collections.Generic;
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

        public override IEnumerable<ICoordinate> Environment
        {
            get
            {
                for (int x = X - 1; x <= X + 1; x++)
                {
                    for (int y = Y - 1; y <= Y + 1; y++)
                    {
                        if (x < 0 || y < 0)
                            continue;
                        
                        var coordinate = new Coordinate2D(x, y);

                        if (!Equals(coordinate))
                        {
                            yield return coordinate;
                        }
                    }
                }
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
            return new Coordinate2D(Coordinates.ToArray());
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
