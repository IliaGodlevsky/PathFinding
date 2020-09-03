using GraphLibrary.GraphFactory;
using ConsoleVersion.Vertex;
using GraphLibrary;
using GraphLibrary.Vertex;
using GraphLibrary.Common.Constants;

namespace ConsoleVersion.GraphFactory
{
    internal class ConsoleGraphInitializer : AbstractGraphInfoInitializer
    {
        public ConsoleGraphInitializer(VertexInfo[,] info) : base(info, VertexSize.SIZE_BETWEEN_VERTICES)
        {

        }

        protected override IVertex CreateVertex(VertexInfo info) => new ConsoleVertex(info);
    }
}
