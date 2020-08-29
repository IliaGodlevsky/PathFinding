using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using GraphLibrary.Graph;
using WinFormsVersion.Vertex;
using WinFormsVersion.Graph;
using GraphLibrary.Common.Constants;

namespace WinFormsVersion.GraphFactory
{
    internal class WinFormsGraphFactory : AbstractGraphFactory
    {
        public WinFormsGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenButtons) : base(percentOfObstacles,
                width, height, placeBetweenButtons)
        {
            
        }

        protected override IVertex CreateGraphTop() => new WinFormsVertex { Text = VertexValueRange.GetRandomVertexValue().ToString() };

        public override AbstractGraph GetGraph() => new WinFormsGraph(vertices);

        protected override void SetGraph(int width, int height) => vertices = new WinFormsVertex[width, height];
    }
}
