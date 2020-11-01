using System;
using System.Collections.Generic;
using GraphLib.Coordinates.Interface;
using GraphLib.Coordinates;
using GraphLib.Info.Containers;
using GraphLib.Info.Interface;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;

namespace GraphLib.Graphs
{
    /// <summary>
    /// A structure amounting to a set of objects in which 
    /// some pairs of the objects are in some sense "related"
    /// </summary>
    [Serializable]
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
                    return new DefaultVertex();
                var coord = coordinate as Coordinate2D;
                if (coord == null)
                    throw new ArgumentException("Must be 2D coordinates");
                return vertices[Converter.ToIndex(coordinate, Length)];
            }
            set
            {
                if (coordinate.IsDefault)
                    return;
                var coord = coordinate as Coordinate2D;
                if (coord == null)
                    throw new ArgumentException("Must be 2D coordinates");
                vertices[Converter.ToIndex(coordinate, Length)] = value;
            }
        }

        private IVertex end;
        public override IVertex End
        {
            get => end;
            set { end = value; end.IsEnd = true; }
        }

        private IVertex start;
        public override IVertex Start
        {
            get => start;
            set { start = value; start.IsStart = true; }
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