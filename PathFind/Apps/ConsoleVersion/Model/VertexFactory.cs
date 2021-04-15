using GraphLib.Interfaces;

namespace ConsoleVersion.Model
{
    internal class VertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new Vertex();
        }
    }
}
