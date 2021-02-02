using GraphLib.Interface;

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
