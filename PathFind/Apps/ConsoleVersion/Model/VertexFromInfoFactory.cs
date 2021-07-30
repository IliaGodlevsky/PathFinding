using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;

namespace ConsoleVersion.Model
{
    internal sealed class VertexFromInfoFactory : IVertexFromInfoFactory
    {
        public IVertex CreateFrom(VertexSerializationInfo info)
        {
            return new Vertex(info);
        }
    }
}