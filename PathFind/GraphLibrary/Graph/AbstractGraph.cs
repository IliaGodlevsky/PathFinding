using System.Collections;
using System.Drawing;
using GraphLibrary.Extensions.MatrixExtension;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Graph
{
    public abstract class AbstractGraph : IEnumerable
    {
        public AbstractGraph(IVertex[,] vertices)
        {
            this.vertices = vertices;
        }

        protected IVertex[,] vertices;

        public IVertex this[int width, int height]
        {
            get { return vertices[width, height]; }
            set { vertices[width, height] = value; }
        }

        public IVertex End { get; set; }
        public IVertex Start { get; set; }

        public IVertex[,] GetArray()
        {
            return vertices;
        }

        public int Height => vertices.Height();

        public VertexInfo[,] Info => vertices.Accumulate(t => t.GetInfo());

        public int Size => vertices.Length;

        public int Width => vertices.Width();

        public void Insert(IVertex vertex)
        {
            var coordiantes = GetIndexes(vertex);
            vertices[coordiantes.X, coordiantes.Y] = vertex;
            NeigbourSetter setter = new NeigbourSetter(vertices);
            setter.SetNeighbours(coordiantes.X, coordiantes.Y);
        }

        public int ObstaclePercent => vertices.CountIf(vertex => vertex.IsObstacle) * 100 / Size;

        public abstract Point GetIndexes(IVertex vertex);
        public abstract void ToDefault(IVertex vertex);

        public void Refresh()
        {
            if (vertices == null)
                return;
            foreach (var vertex in vertices)            
                ToDefault(vertex);            
            Start = null;
            End = null;
        }

        public IEnumerator GetEnumerator()
        {
            return vertices.GetEnumerator();
        }
    }
}