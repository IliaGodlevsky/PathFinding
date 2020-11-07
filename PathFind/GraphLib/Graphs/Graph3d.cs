using GraphLib.Coordinates;
using GraphLib.Coordinates.Interface;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using GraphLib.Info.Interface;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Graphs
{
    public class Graph3d : BaseGraph
    {
        public Graph3d(int width, int lenght, int height)
        {
            Width = width; Length = lenght; Height = height;
            vertices = new IVertex[Size];
            this.RemoveExtremeVertices();
        }

        public override IVertex this[ICoordinate coordinate]
        {
            get
            {
                if (coordinate.IsDefault)
                    return new DefaultVertex();
                var coord = coordinate as Coordinate3D;
                if (coord == null)
                    throw new ArgumentException("Must be 3D coordinates");
                return vertices[Index.ToIndex(coordinate, Length, Height)];
            }
            set
            {
                if (coordinate.IsDefault)
                    return;
                var coord = coordinate as Coordinate3D;
                if (coord == null)
                    throw new ArgumentException("Must be 3D coordinates");
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
