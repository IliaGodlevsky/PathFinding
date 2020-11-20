using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System;
using System.Linq;

namespace GraphLib.Graphs
{
    public sealed class Graph3D : BaseGraph
    {
        public int Width => DimensionsSizes.First();

        public int Length => DimensionsSizes.ElementAt(1);

        public int Height => DimensionsSizes.Last();

        public Graph3D(int width, int lenght, int height)
            : this(new int[] { width, lenght, height })
        {

        }

        public Graph3D(params int[] dimensions) : base(dimensions)
        {
            if (dimensions.Length != 3)
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

                if (coordinate is Coordinate3D)
                {
                    return vertices[Index.ToIndex(coordinate, Length, Height)];
                }

                throw new ArgumentException("Must be 3D coordinates");
            }
            set
            {
                if (coordinate.IsDefault)
                {
                    return;
                }

                if (!(coordinate is Coordinate3D))
                {
                    throw new ArgumentException("Must be 3D coordinates");
                }

                vertices[Index.ToIndex(coordinate, Length, Height)] = value;
            }
        }    

        public override string GetFormattedData(string format)
        {
            return string.Format(format, Width, Length, Height,
                ObstaclePercent, ObstacleNumber, Size);
        }
    }
}
