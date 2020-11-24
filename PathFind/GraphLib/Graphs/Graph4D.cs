using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System;
using System.Linq;

namespace GraphLib.Graphs
{
    public sealed class Graph4D : BaseGraph
    {
        public int Width => DimensionsSizes.First();

        public int Length => DimensionsSizes.ElementAt(1);

        public int Height => DimensionsSizes.ElementAt(2);

        public int FourthDimension => DimensionsSizes.Last();

        public Graph4D(int width, int lenght, int height, int fourth)
            : this(new int[] { width, lenght, height, fourth })
        {

        }

        public Graph4D(params int[] dimensions) : base(dimensions)
        {
            if (dimensions.Length != 4)
            {
                throw new ArgumentException("Number of dimensions doesn't match");
            }
        }

        public override IVertex this[ICoordinate coordinate]
        {
            get
            {
                if (coordinate.IsDefault)
                {
                    return new DefaultVertex();
                }

                if (coordinate is Coordinate4D)
                {
                    int index = Index.ToIndex(coordinate, Length, Height, FourthDimension);
                    return vertices[index];
                }

                throw new ArgumentException("Must be 4D coordinates");
            }
            set
            {
                if (coordinate.IsDefault)
                {
                    return;
                }

                if (!(coordinate is Coordinate4D))
                {
                    throw new ArgumentException("Must be 4D coordinates");
                }

                int index = Index.ToIndex(coordinate, Length, Height, FourthDimension);
                vertices[index] = value;
            }
        }

        public override string GetFormattedData(string format)
        {
            return string.Empty;
        }
    }
}