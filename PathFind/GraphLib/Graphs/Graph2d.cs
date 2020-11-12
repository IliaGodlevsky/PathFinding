using System;
using System.Collections.Generic;
using GraphLib.Coordinates.Interface;
using GraphLib.Coordinates;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;

namespace GraphLib.Graphs
{
    /// <summary>
    /// A structure amounting to a set of objects in which 
    /// some pairs of the objects are in some sense "related"
    /// </summary>
    public class Graph2d : BaseGraph
    {
        public Graph2d(int width, int lenght)
        {
            Width = width; Length = lenght;
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

        public override IVertexInfoCollection VertexInfoCollection => new VertexInfoCollection2D(this, Width, Length);

        public override IEnumerable<int> DimensionsSizes => new int[] { Width, Length };

        public override bool IsDefault => false;

        public override string GetFormattedData(string format)
        {
            return string.Format(format, Width, Length,
               ObstaclePercent, ObstacleNumber, Size);
        }
    }
}