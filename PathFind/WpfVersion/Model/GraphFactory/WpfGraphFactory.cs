using GraphLibrary.Common.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
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

        protected override IVertex CreateVertex() => new WpfVertex { Cost = VertexValueRange.GetRandomVertexCost() };
    }
}
