using GraphLib.Coordinates;
using GraphLib.Coordinates.Interface;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Graphs
{
    public class Graph3d : BaseGraph
    {
        public Graph3d(int width, int lenght, int height)
            : this(new int[] { width, lenght, height })
        {

        }

        public Graph3d(params int[] dimensions)
        {
            if (dimensions.Length != 3)
            {
                throw new ArgumentException("Number of dimensions doesn't match");
            }

            Width = dimensions[0]; 
            Length = dimensions[1]; 
            Height = dimensions[2];

            vertices = new IVertex[Size];
            this.RemoveExtremeVertices();
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

        public int Width { get; private set; }

        public int Length { get; private set; }

        public int Height { get; private set; }

        public override IVertexInfoCollection VertexInfoCollection => new VertexInfoCollection3D(vertices, Width, Length, Height);

        public override IEnumerable<int> DimensionsSizes => new int[] { Width, Length, Height };

        public override bool IsDefault => false;

        public override string GetFormattedData(string format)
        {
            return string.Format(format, Width, Length, Height,
                ObstaclePercent, ObstacleNumber, Size);
        }
    }
}
