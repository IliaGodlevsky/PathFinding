using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Model.Vertex;
using GraphLibrary.Vertex;

namespace GraphLibrary.Collection
{
    public class Graph : IEnumerable<IVertex>
    {
        public Graph(IVertex[,] vertices)
        {
            this.vertices = vertices;
            End = NullVertex.GetInstance();
            Start = NullVertex.GetInstance();
        }

        private readonly IVertex[,] vertices;

        public IVertex this[int width, int height]
        {
            get => vertices[width, height];
            set => vertices[width, height] = value;
        }

        public IVertex End { get; set; }

        public IVertex Start { get; set; }

        public IVertex[,] GetArray() => vertices;

        public int Height => vertices.Height();

        public VertexInfo[,] VerticesInfo => vertices.Accumulate(vertex => vertex.Info);

        public int Size => vertices.Length;

        public int Width => vertices.Width();

        public int ObstaclePercent => Size == 0 ? 0 : ObstacleNumber * 100 / Size;

        public int NumberOfVisitedVertices => vertices.Cast<IVertex>().Count(vertex => vertex.IsVisited);

        public int ObstacleNumber => vertices.Cast<IVertex>().Count(vertex => vertex.IsObstacle);

        public Point GetIndices(IVertex vertex) => vertices.GetIndices(vertex);

        public IEnumerator GetEnumerator() => vertices.GetEnumerator();

        IEnumerator<IVertex> IEnumerable<IVertex>.GetEnumerator() => (IEnumerator<IVertex>)GetEnumerator();
    }
}