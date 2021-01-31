using GraphLib.Vertex.Infrastructure.Factories.Interfaces;
using GraphLib.Vertex.Interface;

namespace ConsoleVersion.Model
{
    internal class VertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new Vertex();
        }
    }
}
