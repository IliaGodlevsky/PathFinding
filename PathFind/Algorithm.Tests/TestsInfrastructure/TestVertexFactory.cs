using GraphLib.Vertex.Infrastructure.Factories.Interfaces;
using GraphLib.Vertex.Interface;

namespace Algorithm.Tests.TestsInfrastructure
{
    internal class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new TestVertex();
        }
    }
}
