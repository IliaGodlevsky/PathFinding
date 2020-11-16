using System;
using System.Collections.Generic;
using GraphLib.Coordinates.Interface;
using GraphLib.Coordinates;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using System.Linq;

namespace GraphLib.Graphs
{
    /// <summary>
    /// A structure amounting to a set of objects in which 
    /// some pairs of the objects are in some sense "related"
    /// </summary>
    public class Graph2D : BaseGraph
    {
        public Graph2D(int width, int length)
            : this(new int[] { width, length })
        {

        }

        public Graph2D(params int[] dimensions)
        {
            if (dimensions.Length != 2)
            {
                throw new ArgumentException("Number of dimensions doesn't match");
            }

            Width = dimensions.FirstOrDefault();
            Length = dimensions.LastOrDefault();

            vertices = new IVertex[Size];
            this.RemoveExtremeVertices();
        }

        public int Width { get; private set; }

        public int Length { get; private set; }

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

                var index = Index.ToIndex(coordinate, Length);
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

                var index = Index.ToIndex(coordinate, Length);
                vertices[index] = value;
            }
        }      

        public override IEnumerable<int> DimensionsSizes => new int[] { Width, Length };

        public override bool IsDefault => false;

        public override string GetFormattedData(string format)
        {
            return string.Format(format, Width, Length,
               ObstaclePercent, ObstacleNumber, Size);
        }
    }
}