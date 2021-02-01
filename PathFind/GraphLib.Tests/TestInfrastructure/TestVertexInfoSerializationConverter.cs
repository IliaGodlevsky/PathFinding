using GraphLib.Graphs.Serialization.Factories.Interfaces;
using GraphLib.Info;
using GraphLib.Vertex.Interface;

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
