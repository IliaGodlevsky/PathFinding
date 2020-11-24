using GraphLib.Coordinates.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            for (int w = W - 1; w <= W + 1; w++)
                            {
                                if (x < 0 || y < 0 || z < 0 || w < 0)
                                    continue;

                                var coordinate = new Coordinate4D(x, y, z, w);

                                if (!Equals(coordinate))
                                {
                                    yield return coordinate;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override object Clone()
        {
            return new Coordinate4D(X, Y, Z, W);
        }
    }
}
