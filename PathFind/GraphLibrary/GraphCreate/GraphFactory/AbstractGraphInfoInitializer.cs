using GraphLibrary.DTO;
using GraphLibrary.Extensions;
using GraphLibrary.Extensions.CollectionExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using System.Drawing;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphInfoInitializer
        : IGraphFactory
    {
        protected IVertex[,] vertices;
        protected int placeBetweenVertices;

        public AbstractGraphInfoInitializer(VertexInfo[,] info, int placeBetweenVertices)            
        {
            this.placeBetweenVertices = placeBetweenVertices;
            vertices = new IVertex[info.Width(), info.Height()];

            if (info == null)
                return;
            IVertex InitializeVertex(IVertex vertex)
            {
                var indices = vertices.GetIndices(vertex);
                vertex = CreateVertex(info[indices.X, indices.Y]);
                vertex.SetLocation(new Point(indices.X * placeBetweenVertices, indices.Y * placeBetweenVertices));
                return vertex;
            }
            vertices.Apply(InitializeVertex);            
        }

        protected abstract IVertex CreateVertex(VertexInfo info);
        public Graph GetGraph() => new Graph(vertices);
       
    }
}
