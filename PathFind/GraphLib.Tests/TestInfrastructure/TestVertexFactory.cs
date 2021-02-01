using GraphLib.Vertex.Infrastructure.Factories.Interfaces;
using GraphLib.Vertex.Interface;

namespace GraphLib.Tests.TestInfrastructure
{
    internal class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new TestVertex();
        }
    }
}
