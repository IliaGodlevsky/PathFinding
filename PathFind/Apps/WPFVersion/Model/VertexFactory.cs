using GraphLib.Interface;

namespace WPFVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new Vertex();
        }
    }
}
