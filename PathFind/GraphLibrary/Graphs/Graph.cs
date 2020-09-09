using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GraphLibrary.DTO;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;

namespace GraphLibrary.Graphs
{
    public class Graph : IGraph
    {
        public Graph(IVertex[,] vertices)
        {
            Array = vertices;
            this.RemoveExtremeVertices();
        }

        public Graph(int width, int height) : this(new IVertex[width, height])
        {

        }

        public IVertex this[int width, int height]
        {
            get => Array[width, height];
            set => Array[width, height] = value;
        }

        public IVertex End { get; set; }

        public IVertex Start { get; set; }

        public IVertex[,] Array { get; }

        public int Height => Array.Height();

        public VertexInfo[,] VerticesInfo => Array.Accumulate(vertex => vertex.Info);

        public int Size => Array.Length;

        public int Width => Array.Width();

        public int ObstaclePercent => Size == 0 ? 0 : ObstacleNumber * 100 / Size;

        public int NumberOfVisitedVertices => Array.Cast<IVertex>().Count(vertex => vertex.IsVisited);

        public int ObstacleNumber => Array.Cast<IVertex>().Count(vertex => vertex.IsObstacle);

        public Point GetIndices(IVertex vertex) => Array.GetIndices(vertex);

        public IEnumerator GetEnumerator() => Array.GetEnumerator();

        IEnumerator<IVertex> IEnumerable<IVertex>.GetEnumerator() => (IEnumerator<IVertex>)Array.GetEnumerator();
    }
}