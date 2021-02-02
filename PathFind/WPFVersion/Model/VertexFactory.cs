using GraphLib.Interface;

namespace WPFVersion.Model
{
    internal class VertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new Vertex();
        }
    }
}
