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

        public override IVertex CreateVertex(VertexInfo info)
        {
            return new ConsoleVertex(info);
        }

        public override AbstractGraph GetGraph()
        {
            return new ConsoleGraph(vertices);
        }

        public override void SetGraph(int width, int height)
        {
            vertices = new ConsoleVertex[width, height];
        }
    }
}
