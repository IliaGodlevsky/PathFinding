using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using GraphLib.TestRealizations.TestObjects;

namespace GraphLib.TestRealizations.TestFactories
{
    public sealed class TestVertexFromInfoFactory : IVertexFromInfoFactory
    {
        public IVertex CreateFrom(VertexSerializationInfo info)
        {
            return new TestVertex(info);
        }
    }
}
