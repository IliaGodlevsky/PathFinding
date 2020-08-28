using GraphLibrary.Constants;
using ConsoleVersion.Graph;
using ConsoleVersion.Vertex;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using GraphLibrary.Graph;
using GraphLibrary.Extensions.RandomExtension;

namespace ConsoleVersion.GraphFactory
{
    public class ConsoleGraphFactory : AbstractGraphFactory
    {
        public ConsoleGraphFactory(int percentOfObstacles,
        int width, int height) : base(percentOfObstacles, width, height, Const.SIZE_BETWEEN_VERTICES)
        {

        }

        protected override IVertex CreateGraphTop() => new ConsoleVertex { Text = rand.GetRandomVertexValue() };

        public override AbstractGraph GetGraph() => new ConsoleGraph(vertices);

        protected override void SetGraph(int width, int height) => vertices = new ConsoleVertex[width, height];
    }
}
