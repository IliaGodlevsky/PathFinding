using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Vertex;
using System.Drawing;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphLocator
    {
        protected IVertex[,] vertices;
        protected int placeBetweenVertices;

        protected AbstractGraphLocator(int placeBetweenVertices)
        {
            this.placeBetweenVertices = placeBetweenVertices;
        }

        protected virtual IVertex SetLocation(IVertex vertex)
        {
            var indexes = vertices.GetIndices(vertex);
            vertex.Location = new Point(
                indexes.X * placeBetweenVertices,
                indexes.Y * placeBetweenVertices);
            return vertex;
        }

        protected abstract void SetGraph(int width, int height);
    }
}
