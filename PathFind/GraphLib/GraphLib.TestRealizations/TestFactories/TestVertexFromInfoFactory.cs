using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using GraphLib.TestRealizations.TestObjects;

namespace GraphLib.TestRealizations.TestFactories
{
    public sealed class TestVertexFromInfoFactory : IVertexFromInfoFactory<TestVertex>
    {
        public TestVertex CreateFrom(VertexSerializationInfo info)
        {
            return new TestVertex(info);
        }
    }
}
