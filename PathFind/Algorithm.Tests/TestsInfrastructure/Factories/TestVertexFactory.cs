using Algorithm.Tests.TestsInfrastructure.Objects;
using GraphLib.Interface;

namespace Algorithm.Tests.TestsInfrastructure.Factories
{
    internal class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new TestVertex();
        }
    }
}
