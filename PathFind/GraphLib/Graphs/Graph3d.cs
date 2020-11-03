using GraphLib.Coordinates;
using GraphLib.Coordinates.Interface;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Info.Containers;
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
                    throw new ArgumentException("Must be 2D coordinates");
                var c = coordinate as Coordinate3D;
                var index = c.X * Height * Length + c.Y * Height + c.Z;
                return vertices[index];
            }
            set
            {
                if (coordinate.IsDefault)
                    return;
                var coord = coordinate as Coordinate3D;
                if (coord == null)
                    throw new ArgumentException("Must be 2D coordinates");
                var c = coordinate as Coordinate3D;
                var index = c.X * Height * Length + c.Y * Height + c.Z;
                vertices[index] = value;
            }
        }

        public int Width { get; private set; }
        public int Length { get; private set; }
        public int Height { get; private set; }

        public override IVertexInfoCollection VertexInfoCollection => new EmptyVertexInfoCollection();

        public override IEnumerable<int> DimensionsSizes => new int[] { Width, Length, Height };

        public override bool IsDefault => false;

        public override string GetFormattedData(string format)
        {
            return string.Empty;
        }
    }
}
