using GraphLibrary.Common.Constants;
using GraphLibrary.Graph;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using WpfVersion.Model.Graph;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.GraphFactory
{
    internal class WpfGraphFactory : AbstractGraphFactory
    {
        public WpfGraphFactory(int percentOfObstacles, 
            int width, int height, int placeBetweenVertices) : 
            base(percentOfObstacles, width, height, placeBetweenVertices)
        {
        }

        public override AbstractGraph GetGraph() => new WpfGraph(vertices);

        protected override IVertex CreateVertex() => new WpfVertex { Cost = VertexValueRange.GetRandomVertexValue() };
    }
}
