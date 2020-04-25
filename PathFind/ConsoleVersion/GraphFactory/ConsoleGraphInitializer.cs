using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using ConsoleVersion;
using ConsoleVersion.Graph;
using ConsoleVersion.Vertex;
using GraphLibrary;
using GraphLibrary.Vertex;
using GraphLibrary.Graph;

namespace ConsoleVersion.GraphFactory
{
    public class ConsoleGraphInitializer : AbstractGraphInitializer
    {
        public ConsoleGraphInitializer(VertexInfo[,] info) : base(info, Const.SIZE_BETWEEN_VERTICES)
        {

        }

        protected override IVertex CreateVertex(VertexInfo info) => new ConsoleVertex(info);

        public override AbstractGraph GetGraph() => new ConsoleGraph(vertices);

        protected override void SetGraph(int width, int height) => vertices = new ConsoleVertex[width, height];
    }
}
