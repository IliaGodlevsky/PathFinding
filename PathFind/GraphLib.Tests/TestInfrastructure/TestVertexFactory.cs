using GraphLib.Interface;

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
