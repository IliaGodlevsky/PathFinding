using GraphLib.Infrastructure;
using GraphLib.Interface;

namespace GraphLib.Tests.TestInfrastructure
{
    public class TestVertexInfoSerializationConverter : IVertexSerializationInfoConverter
    {
        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            return new TestVertex(info);
        }
    }
}
