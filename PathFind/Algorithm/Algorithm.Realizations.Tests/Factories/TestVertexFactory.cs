using Algorithm.Realizations.Tests.Objects;
using GraphLib.Interface;

namespace Algorithm.Realizations.Tests.Factories
{
    internal class TestVertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new TestVertex();
        }
    }
}
