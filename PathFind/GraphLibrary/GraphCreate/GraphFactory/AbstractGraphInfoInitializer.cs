using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Graph;
using GraphLibrary.Vertex;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphInfoInitializer
        : AbstractVertexLocator, IGraphFactory
    {
        public AbstractGraphInfoInitializer(VertexInfo[,] info,
            int placeBetweenVertices) : base(placeBetweenVertices)
        {

            if (info == null)
                return;

            SetGraph(info.Width(), info.Height());
            vertices.Apply(vertex => CreateVertex(
                info[vertices.GetIndices(vertex).X, 
                vertices.GetIndices(vertex).Y]));
            vertices.Apply(SetLocation);
        }

        protected abstract IVertex CreateVertex(VertexInfo info);
        public abstract AbstractGraph GetGraph();
       
    }
}
