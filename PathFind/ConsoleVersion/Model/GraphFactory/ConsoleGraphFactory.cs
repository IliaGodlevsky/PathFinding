using ConsoleVersion.Graph;
using ConsoleVersion.Vertex;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using GraphLibrary.Graph;
using GraphLibrary.Common.Constants;

namespace ConsoleVersion.GraphFactory
{
    internal class ConsoleGraphFactory : AbstractGraphFactory
    {
        public ConsoleGraphFactory(int percentOfObstacles,
        int width, int height) : base(percentOfObstacles, width, height, VertexSize.SIZE_BETWEEN_VERTICES)
        {

        }

        protected override IVertex CreateGraphTop() => new ConsoleVertex { Cost = VertexValueRange.GetRandomVertexValue() };

        public override AbstractGraph GetGraph() => new ConsoleGraph(vertices);

        protected override void SetGraph(int width, int height) => vertices = new ConsoleVertex[width, height];
    }
}
