using ConsoleVersion.Vertex;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using GraphLibrary.Common.Constants;

namespace ConsoleVersion.GraphFactory
{
    internal class ConsoleGraphFactory : AbstractGraphFactory
    {
        public ConsoleGraphFactory(int percentOfObstacles,
        int width, int height) : base(percentOfObstacles, width, height, VertexSize.SIZE_BETWEEN_VERTICES)
        {

        }

        protected override IVertex CreateVertex() => new ConsoleVertex { Cost = VertexValueRange.GetRandomVertexCost() };
    }
}
