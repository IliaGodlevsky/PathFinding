using System;
using GraphLib.Coordinates;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using GraphLib.Graphs.Abstractions;
using System.Linq;
using GraphLib.Coordinates.Abstractions;

namespace GraphLib.Graphs
{
    /// <summary>
    /// A structure amounting to a set of objects in which 
    /// some pairs of the objects are in some sense "related"
    /// </summary>
    public sealed class Graph2D : BaseGraph
    {
        public int Width => DimensionsSizes.First();

        public int Length => DimensionsSizes.Last();

        public Graph2D(int width, int length)
            : this(new int[] { width, length })
        {

        }

        public Graph2D(params int[] dimensions)
            :base(dimensions)
        {
            if (dimensions.Length != 2)
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

                if (!(coordinate is Coordinate2D))
                {
                    throw new ArgumentException("Must be 2D coordinates");
                }

                int index = Index.ToIndex(coordinate, Length);
                return vertices[index];
            }
            set
            {
                if (coordinate.IsDefault)
                    return;

                if (!(coordinate is Coordinate2D))
                {
                    throw new ArgumentException("Must be 2D coordinates");
                }

                int index = Index.ToIndex(coordinate, Length);
                vertices[index] = value;
            }
        }

        public override string GetFormattedData(string format)
        {
            return string.Format(format, Width, Length,
               ObstaclePercent, ObstacleNumber, Size);
        }
    }
}