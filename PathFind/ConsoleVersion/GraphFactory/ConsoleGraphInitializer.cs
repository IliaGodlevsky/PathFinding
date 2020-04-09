using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using SearchAlgorythms;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

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
