using GraphLibrary.GraphFactory;
using GraphLibrary.DTO;
using GraphLibrary.Vertex.Interface;
using ConsoleVersion.Model.Vertex;
using GraphLibrary.Constants;

namespace ConsoleVersion.Model.GraphFactory
{
    internal class ConsoleGraphInitializer : AbstractGraphInfoInitializer
    {
        public ConsoleGraphInitializer(VertexInfo[,] info) : base(info, VertexSize.SIZE_BETWEEN_VERTICES)
        {

        }

        protected override IVertex CreateVertex(VertexInfo info) => new ConsoleVertex(info);
    }
}
