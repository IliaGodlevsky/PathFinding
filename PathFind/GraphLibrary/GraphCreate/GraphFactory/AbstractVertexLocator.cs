using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Vertex;
using System.Drawing;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractVertexLocator
    {
        protected IVertex[,] vertices;
        protected int placeBetweenVertices;

        protected AbstractVertexLocator(int placeBetweenVertices)
        {
            this.placeBetweenVertices = placeBetweenVertices;
        }

        protected virtual IVertex SetLocation(IVertex vertex)
        {
            var indices = vertices.GetIndices(vertex);
            vertex.Location = new Point(
                indices.X * placeBetweenVertices,
                indices.Y * placeBetweenVertices);
            return vertex;
        }

        protected abstract void SetGraph(int width, int height);
    }
}
