using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphLibrary.Coordinates;
using GraphLibrary.Coordinates.Interface;
using GraphLibrary.DTO;
using GraphLibrary.DTO.Interface;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;

namespace GraphLibrary.Graphs
{
    /// <summary>
    /// A structure amounting to a set of objects in which 
    /// some pairs of the objects are in some sense "related"
    /// </summary>
    [Serializable]
    public class Graph : IGraph
    {
        public Graph(int width, int lenght)
        {
            Width = width; Length = lenght;
            vertices = new IVertex[Size];
            this.RemoveExtremeVertices();
        }

        public int Width { get; private set; }

        public int Length { get; private set; }

        public IVertex this[ICoordinate coordinate]
        {
            get
            {
                if (ReferenceEquals(coordinate, NullCoordinate.Instance))
                    return NullVertex.Instance;
                var coord = coordinate as Coordinate2D;
                if (coord == null)
                    throw new ArgumentException("Must be 2D coordinates");
                return vertices[Converter.ToIndex(coordinate, Length)];
            }
            set
            {
                if (ReferenceEquals(coordinate, NullCoordinate.Instance))
                    return;
                var coord = coordinate as Coordinate2D;
                if (coord == null)
                    throw new ArgumentException("Must be 2D coordinates");
                vertices[Converter.ToIndex(coordinate, Length)] = value;
            }
        }
        public int Size => Width * Length;

        public int NumberOfVisitedVertices => vertices.AsParallel().Count(vertex => vertex.IsVisited);

        public int ObstacleNumber => vertices.AsParallel().Count(vertex => vertex.IsObstacle);

        public int ObstaclePercent => Size == 0 ? 0 : ObstacleNumber * 100 / Size;

        private IVertex end;
        public IVertex End
        {
            get => end;
            set { end = value; end.IsEnd = true; }
        }

        private IVertex start;
        public IVertex Start
        {
            get => start;
            set { start = value; start.IsStart = true; }
        }

        public IVertexDtoContainer VertexDtos => new VertexDtoContainer2D(this, Width, Length);

        public IEnumerable<int> DimensionsSizes => new int[] { Width, Length };

        public IEnumerator<IVertex> GetEnumerator()
        {
            return vertices.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return vertices.AsEnumerable().GetEnumerator();
        }

        public string GetFormattedData(string format)
        {
            return string.Format(format, Width, Length,
               ObstaclePercent, ObstacleNumber, Size);
        }

        private readonly IVertex[] vertices;
    }
}