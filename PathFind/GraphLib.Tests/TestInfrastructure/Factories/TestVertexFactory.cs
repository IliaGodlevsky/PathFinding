using GraphLib.Interface;
using GraphLib.Tests.TestInfrastructure.Objects;

namespace GraphLib.Tests.TestInfrastructure.Factories
{
    internal class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new TestVertex();
        }
    }
}
