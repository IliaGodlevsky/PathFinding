using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphLibrary.DTO;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;

namespace GraphLibrary.Graphs
{
    /// <summary>
    /// A structure amounting to a set of objects in which 
    /// some pairs of the objects are in some sense "related"
    /// </summary>
    public class Graph : IGraph
    {
        public Graph(IVertex[,] vertices)
        {
            Array = vertices;
            this.RemoveExtremeVertices();
        }

        public Graph(int width, int height) 
            : this(new IVertex[width, height])
        {

        }

        public IVertex this[int width, int height]
        {
            get => Array[width, height];
            set => Array[width, height] = value;
        }

        public IVertex this[Position position] 
        {
            get => Array[position.X, position.Y];
            set => Array[position.X, position.Y] = value; 
        }

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

        public IVertex[,] Array { get; }

        public int Height => Array.Height();

        public VertexDto[,] VerticesDto => Array.Accumulate(vertex => vertex.Dto);

        public int Size => Array.Length;

        public int Width => Array.Width();

        public int ObstaclePercent => Size == 0 ? 0 : ObstacleNumber * 100 / Size;

        public int NumberOfVisitedVertices => Array.Cast<IVertex>().AsParallel().Count(vertex => vertex.IsVisited);

        public int ObstacleNumber => Array.Cast<IVertex>().AsParallel().Count(vertex => vertex.IsObstacle);

        public Position GetIndices(IVertex vertex) => Array.GetIndices(vertex);

        public IEnumerator GetEnumerator() => Array.GetEnumerator();

        IEnumerator<IVertex> IEnumerable<IVertex>.GetEnumerator() => Array.Cast<IVertex>().ToList().GetEnumerator();
    }
}