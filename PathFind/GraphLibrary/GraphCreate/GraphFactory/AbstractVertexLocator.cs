using GraphLibrary.Vertex;
using System.Drawing;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractVertexLocator
    {
        protected IVertex[,] vertices;
        protected int placeBetweenVertices;
        protected Point indices;

        protected AbstractVertexLocator(int width, int height, int placeBetweenVertices)
        {
            this.placeBetweenVertices = placeBetweenVertices;
            vertices = new IVertex[width, height];
        }

        protected virtual IVertex SetLocation(IVertex vertex)
        {
            vertex.Location = new Point(
                    indices.X * placeBetweenVertices,
                    indices.Y * placeBetweenVertices);
            return vertex;
        }
    }
}
