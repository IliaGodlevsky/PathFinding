using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex.Interface;
using ConsoleVersion.Model.Vertex;
using GraphLibrary.Constants;

namespace ConsoleVersion.Model.GraphFactory
{
    internal class ConsoleGraphFactory : AbstractGraphFactory
    {
        public ConsoleGraphFactory(int percentOfObstacles,
        int width, int height) : base(percentOfObstacles, width, height, VertexSize.SIZE_BETWEEN_VERTICES)
        {

        }

        protected override IVertex CreateVertex() => new ConsoleVertex();
    }
}
