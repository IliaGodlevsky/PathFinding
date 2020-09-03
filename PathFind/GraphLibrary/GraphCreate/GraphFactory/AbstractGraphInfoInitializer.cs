using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Collection;
using GraphLibrary.Vertex;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphInfoInitializer
        : AbstractVertexLocator, IGraphFactory
    {
        public AbstractGraphInfoInitializer(VertexInfo[,] info,
            int placeBetweenVertices) : 
            base(info.Width(), info.Height(), placeBetweenVertices)
        {
            if (info == null)
                return;
            IVertex InitializeVertex(IVertex vertex)
            {
                indices = vertices.GetIndices(vertex);
                vertex = CreateVertex(info[indices.X, indices.Y]);                
                return SetLocation(vertex);
            }
            vertices.Apply(InitializeVertex);            
        }

        protected abstract IVertex CreateVertex(VertexInfo info);
        public Graph GetGraph() => new Graph(vertices);
       
    }
}
