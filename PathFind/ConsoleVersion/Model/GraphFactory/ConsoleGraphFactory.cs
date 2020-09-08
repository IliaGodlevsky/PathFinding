using GraphLibrary.GraphFactory;
using GraphLibrary.Common.Constants;
using GraphLibrary.Vertex.Interface;
using ConsoleVersion.Model.Vertex;

namespace ConsoleVersion.Model.GraphFactory
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
