using System.Collections;
using System.Drawing;
using System.Linq;
using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Vertex;

namespace GraphLibrary.Graph
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
            get => vertices[width, height];
            set => vertices[width, height] = value;
        }

        public IVertex End { get; set; }
        public IVertex Start { get; set; }

        public IVertex[,] GetArray()
        {
            return vertices;
        }

        public int Height => vertices.Height();

        public VertexInfo[,] Info => vertices.Accumulate(vertex => vertex.Info);

        public int Size => vertices.Length;

        public int Width => vertices.Width();

        public void Insert(IVertex vertex)
        {
            var coordiantes = GetIndices(vertex);
            vertices[coordiantes.X, coordiantes.Y] = vertex;
            var setter = new NeigbourSetter(vertices);
            setter.SetNeighbours(coordiantes.X, coordiantes.Y);
        }

        public int ObstaclePercent => ObstacleNumber * 100 / Size;

        public int ObstacleNumber => vertices.Cast<IVertex>().Count(vertex => vertex.IsObstacle);

        public virtual Point GetIndices(IVertex vertex) => vertices.GetIndices(vertex);

        protected abstract void ToDefault(IVertex vertex);

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