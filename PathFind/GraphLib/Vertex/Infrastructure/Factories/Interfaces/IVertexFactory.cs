using GraphLib.Vertex.Interface;

namespace GraphLib.Vertex.Infrastructure.Factories.Interfaces
{
    public interface IVertexFactory
    {
        IVertex CreateVertex();
    }
}
