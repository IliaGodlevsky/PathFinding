using GraphLibrary;
using GraphLibrary.Graph;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using WpfVersion.Model.Graph;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.GraphFactory
{
    public class WpfGraphInitializer : AbstractGraphInitializer
    {
        public WpfGraphInitializer(VertexInfo[,] info, int placeBetweenVertices) : base(info, placeBetweenVertices)
        {
        }

        public override AbstractGraph GetGraph()
        {
            return new WpfGraph(vertices);
        }

        protected override IVertex CreateVertex(VertexInfo info)
        {
            return new WpfVertex(info);
        }

        protected override void SetGraph(int width, int height)
        {
            vertices = new WpfVertex[width, height];
        }
    }
}
