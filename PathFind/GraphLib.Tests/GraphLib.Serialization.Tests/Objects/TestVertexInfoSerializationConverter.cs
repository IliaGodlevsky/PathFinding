using GraphLib.Interfaces;
using GraphLib.Serialization.Interfaces;

namespace GraphLib.Serialization.Tests.Objects
{
    public class TestVertexInfoSerializationConverter : IVertexSerializationInfoConverter
    {
        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            return new TestVertex(info);
        }
    }
}
