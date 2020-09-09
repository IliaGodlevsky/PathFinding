using GraphLibrary.DTO;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using System.Drawing;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphInfoInitializer : IGraphFactory
    {
        public Graph Graph { get; private set; }

        public AbstractGraphInfoInitializer(VertexInfo[,] info, 
            int placeBetweenVertices)
        {
            Graph = new Graph(info.Width(), info.Height());

            if (info == null)
                return;
            IVertex InitializeVertex(IVertex vertex)
            {
                var indices = Graph.GetIndices(vertex);
                vertex = CreateVertex(info[indices.X, indices.Y]);
                vertex.SetLocation(new Point(indices.X * placeBetweenVertices, 
                    indices.Y * placeBetweenVertices));
                return vertex;
            }
            Graph.Array.Apply(InitializeVertex);
        }

        protected abstract IVertex CreateVertex(VertexInfo info);
    }
}
