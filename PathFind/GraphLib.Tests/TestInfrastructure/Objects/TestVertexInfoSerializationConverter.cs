using GraphLib.Infrastructure;
using GraphLib.Interface;

namespace GraphLib.Tests.TestInfrastructure.Objects
{
    public class TestVertexInfoSerializationConverter : IVertexSerializationInfoConverter
    {
        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            return new TestVertex(info);
        }
    }
}
